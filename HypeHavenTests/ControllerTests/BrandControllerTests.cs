using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HypeHaven.Controllers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.ViewModels.BrandViewModels;
//using HypeHaven.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using HypeHaven.Areas.Identity.Data;
using HypeHaven.Helpers;
using HypeHaven.NewFolder;
using HypeHaven.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using FakeItEasy;
using CloudinaryDotNet.Actions;

namespace HypeHavenTests.ControllerTests
{
    public class BrandControllerTests
    {
        private readonly Mock<IBrandRepository> _brandRepositoryMock;
        private readonly Mock<IRepository<Category>> _categoryRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IPhotoService> _photoServiceMock;

        private readonly BrandController _brandController;

        public BrandControllerTests()
        {
            _brandRepositoryMock = new Mock<IBrandRepository>();
            _categoryRepositoryMock = new Mock<IRepository<Category>>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _photoServiceMock = new Mock<IPhotoService>();

            _brandController = new BrandController(
                _brandRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _httpContextAccessorMock.Object,
                _productRepositoryMock.Object,
                _photoServiceMock.Object
            );
        }

        [Fact] //indicating for xunit that this is a test
        public async Task Index_ReturnsViewWithBrands() //tests index action method of BrandController returning a view with list of brands
        {
            // Arrange - setting up the conditions and objects needed for the test, this is expected to be retunres(a list of brands)
            var brands = new List<Brand> { new Brand(), new Brand() };
            _brandRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(brands); //mocking the repository(we avoid hitting the db), repo simulates the repository, 

            // Act - executing the code we are testing
            var result = await _brandController.Index();

            // Assert - verify the outcome of the action
            var viewResult = Assert.IsType<ViewResult>(result); //asserting that the result is of type ViewResult(expect for index to return the view)
            var model = Assert.IsAssignableFrom<IEnumerable<Brand>>(viewResult.ViewData.Model); //asserting that the model is of type IEnumerable<Brand>
            Assert.Equal(brands.Count, model.Count()); //checks if the number of brands in the model is equal to the number of brands in the list(ensures that index returns the expected number of brands)
        }

        [Fact]
        public async Task MyBrandIndex_ReturnsViewWithBrands()
        {
            // Arrange
            var brands = new List<Brand> { new Brand(), new Brand() };
            _brandRepositoryMock.Setup(repo => repo.GetAllForSpecifedUser()).ReturnsAsync(brands);
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

            // Act
            var result = await _brandController.MyBrandIndex();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Brand>>(viewResult.ViewData.Model);
            Assert.Equal(brands.Count, model.Count());
        }

        [Fact]
        public async Task Detail_ReturnsViewWithBrand()
        {
            // Arrange
            var brand = new Brand { BrandId = 1 };
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.BrandId)).ReturnsAsync(brand);

            // Act
            var result = await _brandController.Detail(brand.BrandId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Brand>(viewResult.ViewData.Model);
            Assert.Equal(brand, model);
        }

        [Fact]
        public async Task Create_ReturnsViewWithBrandViewModel()
        {
            // Arrange
            var categories = new List<Category> { new Category(), new Category() };
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(categories);

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, "1"), //represents an authethicated user
            }, "TestAuthentication"); //"TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); //creating new authethicated user 
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //simulates authenticated user during the create test

            // Act
            var result = await _brandController.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); //Is of type viewresult = the action method returns a view
            var model = Assert.IsAssignableFrom<CreateBrandViewModel>(viewResult.ViewData.Model); //If the correct view model is being used<createbrandviewmodel>
            Assert.Equal(categories.Count, model.Categories.Count()); //checking if it the action is correctly populating viewmodel
        }

        [Fact]
        public async Task Create_WithValidModel_AddsBrandAndRedirectsToIndex()
        {
            // Arrange
            var brandVM = new CreateBrandViewModel { CategoryId = 1 };
            var brand = new Brand();

            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://example.com") };

            // Define a list to hold the brands
            var brands = new List<Brand>();

            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(brandVM.CategoryId)).ReturnsAsync(new Category());
            _photoServiceMock.Setup(x => x.AddPhotoAsync(brandVM.Image)).ReturnsAsync(imageUploadResult);

            _brandRepositoryMock.Setup(repo => repo.Add(It.IsAny<Brand>()))
                .Callback<Brand>(brand => brands.Add(brand));

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, "1"), // represents an authenticated user
            }, "TestAuthentication"); // "TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); // creating a new authenticated user
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); // simulates an authenticated user during the create test

            // Act
            var result = await _brandController.Create(brandVM);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            _brandRepositoryMock.Verify(repo => repo.Add(It.IsAny<Brand>()), Moq.Times.Once);     // Verify that the Add method on the brand repository is called once
            Assert.Single(brands);     // Check that the brand has been added to the collection
            redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


        [Fact]
        public async Task Create_WithInvalidModel_ReturnsViewWithBrandViewModel()
        {
            // Arrange
            var brandVM = new CreateBrandViewModel { CategoryId = 1 };
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Category> { new Category() });
            _brandController.ModelState.AddModelError("Name", "Required");

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, "1"), //represents an authethicated user
            }, "TestAuthentication"); //"TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); //creating new authethicated user 
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //simulates authenticated user during the create test

            // Act
            var result = await _brandController.Create(brandVM);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateBrandViewModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Categories.Count());
        }

        [Fact]
        public async Task Edit_ReturnsViewWithBrandViewModel()
        {
            // Arrange
            var brand = new Brand { BrandId = 1, UserId = "1" };
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.BrandId)).ReturnsAsync(brand);
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.CategoryId)).ReturnsAsync(new Category());
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Category> { new Category() });

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, "1"), //represents an authethicated user
            }, "TestAuthentication"); //"TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); //creating new authethicated user 
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //simulates authenticated user during the create test
            // Act
            var result = await _brandController.Edit(brand.BrandId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditBrandViewModel>(viewResult.ViewData.Model);
            Assert.Equal(brand.Name, model.Name);
        }

        [Fact]
        public async Task Edit_WithValidModel_UpdatesBrandAndRedirectsToIndex()
        {
            // Arrange
            var brandVM = new EditBrandViewModel { CategoryId = 1 };
            var brand = new Brand { BrandId = 1, UserId = "1" };

            //set up the repository and photo service for mocking
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsyncNoTracking(brand.BrandId)).ReturnsAsync(brand);
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsyncNoTracking(brandVM.CategoryId)).ReturnsAsync(new Category());

            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://example.com") };
            _photoServiceMock.Setup(x => x.AddPhotoAsync(brandVM.Image)).ReturnsAsync(imageUploadResult);

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
             new Claim(ClaimTypes.NameIdentifier, "1"), // represents an authenticated user
            }, "TestAuthentication");

            var user = new ClaimsPrincipal(identity);
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user);

            // Act
            var result = await _brandController.Edit(brand.BrandId, brandVM);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result); //is of type redirecttoactionresult
            Assert.Equal("Index", redirectToActionResult.ActionName); //redirects to index                                               ==> working method = working test
            _brandRepositoryMock.Verify(repo => repo.Update(It.IsAny<Brand>()), Moq.Times.Once); //verift that update is called once
        }


        [Fact]
        public async Task Edit_WithInvalidModel_ReturnsViewWithBrandViewModel()
        {
            // Arrange
            var brandVM = new EditBrandViewModel { CategoryId = 1 };
            var brand = new Brand { BrandId = 1, UserId = "1" };
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsyncNoTracking(brand.BrandId)).ReturnsAsync(brand);
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Category> { new Category() });
            _brandController.ModelState.AddModelError("Name", "Required");

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, "1"), //represents an authethicated user
            }, "TestAuthentication"); //"TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); //creating new authethicated user 
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //simulates authenticated user during the create test

            // Act
            var result = await _brandController.Edit(brand.BrandId, brandVM);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EditBrandViewModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Categories.Count());
        }

        [Fact]
        public async Task Delete_ReturnsViewWithBrandViewModel()
        {
            // Arrange
            var brand = new Brand { BrandId = 1, UserId = "1" };
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.BrandId)).ReturnsAsync(brand);
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.CategoryId)).ReturnsAsync(new Category());
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Category> { new Category() });

            // Mock the user's identity to be authenticated and provide a user ID.
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, "1"), //represents an authethicated user
            }, "TestAuthentication"); //"TestAuthentication" is used to distinguish different authentication methods

            var user = new ClaimsPrincipal(identity); //creating new authethicated user 
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //simulates authenticated user during the create test

            // Act
            var result = await _brandController.Delete(brand.BrandId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<DeleteBrandViewModel>(viewResult.ViewData.Model);
            Assert.Equal(brand.Name, model.Name);
        }

        [Fact]
        public async Task DeleteBrand_DeletesBrandAndRedirectsToMyBrandIndex()
        {
            // Arrange
            var brand = new Brand { BrandId = 1, UserId = "1" };
            _brandRepositoryMock.Setup(repo => repo.GetByIdAsync(brand.BrandId)).ReturnsAsync(brand);

            // Act
            var result = await _brandController.DeleteBrand(brand.BrandId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("MyBrandIndex", redirectToActionResult.ActionName);
            _brandRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Brand>()), Moq.Times.Once);
        }

        [Fact]
        public async Task Search_ReturnsViewWithBrands()
        {
            // Arrange
            var brands = new List<Brand> { new Brand(), new Brand() };
            _brandRepositoryMock.Setup(repo => repo.Search(It.IsAny<string>())).ReturnsAsync(brands);

            // Act
            var result = await _brandController.Search("searchTerm");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<(IEnumerable<Brand>, string)>(viewResult.ViewData.Model);
            Assert.Equal(brands.Count, model.Item1.Count());
        }
    }
}

