using HypeHaven.Areas.Identity.Data;
using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.NewFolder;
using HypeHaven.ViewModels;
using HypeHaven.ViewModels.BrandViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Security.Claims;


namespace HypeHaven.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandController"/> class.
        /// </summary>
        /// <param name="brandRepository">The brand repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="photoService">The photo service.</param>
        public BrandController(IBrandRepository brandRepository, IRepository<Category> categoryRepository, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IPhotoService photoService)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _photoService = photoService;
        }

        /// <summary>
        /// Returns the view for the Index page.
        /// </summary>
        /// <returns>The Index view.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAll();
            return View(brands);
        }

        /// <summary>
        /// Returns the view for the MyBrandIndex page.
        /// </summary>
        /// <returns>The MyBrandIndex view.</returns>
        [HttpGet]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> MyBrandIndex()
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAllForSpecifedUser();
            return View(brands);
        }

        /// <summary>
        /// Returns the view for the Detail page.
        /// </summary>
        /// <param name="id">The ID of the brand to display.</param>
        /// <returns>The Detail view.</returns>
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Brand brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            return View(brand);
        }

        /// <summary>
        /// Returns the view for the Create page.
        /// </summary>
        /// <returns>The Create view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var brandViewModel = new CreateBrandViewModel
            {
                Categories = (List<Category>)await _categoryRepository.GetAll(),
                Id = currentUserId
            };
            return View(brandViewModel);
        }

        /// <summary>
        /// Creates a new brand.
        /// </summary>
        /// <param name="brandVM">The view model containing the brand data.</param>
        /// <returns>The Index view.</returns>
        [HttpPost]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Create(CreateBrandViewModel brandVM)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(brandVM.CategoryId);
                var result = await _photoService.AddPhotoAsync(brandVM.Image);


                var brand = new Brand
                {
                    Name = brandVM.Name,
                    Description = brandVM.Description,
                    Location = brandVM.Location,
                    Image = result.Url.ToString(),
                    Email = brandVM.Email,
                    PhoneNumber = brandVM.PhoneNumber,
                    Instagram = brandVM.Instagram,
                    Facebook = brandVM.Facebook,
                    Pinterest = brandVM.Pinterest,
                    Tiktok = brandVM.Tiktok,
                    Video = brandVM.Video,
                    CategoryId = category.CategoryId,
                    UserId = currentUserId,
                    Id = currentUserId

                };
                _brandRepository.Add(brand);
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                brandVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                return View(brandVM);
            }
            return View(brandVM);
        }

        /// <summary>
        /// Returns the view for the Edit page.
        /// </summary>
        /// <param name="id">The ID of the brand to edit.</param>
        /// <returns>The Edit view.</returns>
        [HttpGet]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) return View("Error");

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            //check if currentuser id is of user id who created the brand
            if (brand.UserId == currentUserId)
            {
                var category = await _categoryRepository.GetByIdAsync(brand.CategoryId);

                var brandVM = new EditBrandViewModel
                {
                    Name = brand.Name,
                    Description = brand.Description,
                    Location = brand.Location,
                    URL = brand.Image,
                    Email = brand.Email,
                    PhoneNumber = brand.PhoneNumber,
                    Instagram = brand.Instagram,
                    Facebook = brand.Facebook,
                    Pinterest = brand.Pinterest,
                    Tiktok = brand.Tiktok,
                    Video = brand.Video,
                    CategoryId = category.CategoryId,
                    Categories = (List<Category>)await _categoryRepository.GetAll(),
                    Id = currentUserId
                };
                return View(brandVM);

            }
            //if no redirect to index
            return RedirectToAction("Index");

        }

        /// <summary>
        /// Edits an existing brand.
        /// </summary>
        /// <param name="id">The ID of the brand to edit.</param>
        /// <param name="brandVM">The view model containing the updated brand data.</param>
        /// <returns>The Index view.</returns>
        [HttpPost]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Edit(int id, EditBrandViewModel brandVM)
        {
            var curBrand = await _brandRepository.GetByIdAsyncNoTracking(id);
            if (curBrand == null) return View("Error");
            if (!string.IsNullOrEmpty(curBrand.Image))
            {
                _ = _photoService.DeletePhotoAsync(curBrand.Image);
            }

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var result = await _photoService.AddPhotoAsync(brandVM.Image);

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsyncNoTracking(brandVM.CategoryId);

                curBrand.Name = brandVM.Name;
                curBrand.Description = brandVM.Description;
                curBrand.Location = brandVM.Location;
                curBrand.Image = result.Url.ToString();
                curBrand.Email = brandVM.Email;
                curBrand.PhoneNumber = brandVM.PhoneNumber;
                curBrand.Instagram = brandVM.Instagram;
                curBrand.Facebook = brandVM.Facebook;
                curBrand.Pinterest = brandVM.Pinterest;
                curBrand.Tiktok = brandVM.Tiktok;
                curBrand.Video = brandVM.Video;
                curBrand.CategoryId = category.CategoryId;
                curBrand.Id = currentUserId;

                _brandRepository.Update(curBrand);
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                brandVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                return View(brandVM);
            }

            return View(brandVM);
        }

        /// <summary>
        /// Returns the view for the Delete page.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>The Delete view.</returns>
        [HttpGet]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) return View("Error");

            var category = await _categoryRepository.GetByIdAsync(brand.CategoryId);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (brand.UserId == currentUserId)
            {
                var brandVM = new DeleteBrandViewModel
                {
                    Name = brand.Name,
                    Description = brand.Description,
                    Location = brand.Location,
                    URL = brand.Image,
                    Email = brand.Email,
                    PhoneNumber = brand.PhoneNumber,
                    Instagram = brand.Instagram,
                    Facebook = brand.Facebook,
                    Pinterest = brand.Pinterest,
                    Tiktok = brand.Tiktok,
                    Video = brand.Video,
                    CategoryId = category.CategoryId,
                    Categories = (List<Category>)await _categoryRepository.GetAll(),
                    Id = currentUserId
                };
                return View(brandVM);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes an existing brand.
        /// </summary>
        /// <param name="id">The ID of the brand to delete.</param>
        /// <returns>The MyBrandIndex view.</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            if (!string.IsNullOrEmpty(brand.Image))
            {
                _ = _photoService.DeletePhotoAsync(brand.Image);
            }
            _brandRepository.Delete(brand);
            return RedirectToAction("MyBrandIndex");
        }

        /// <summary>
        /// Returns the view for the Search page.
        /// </summary>
        /// <param name="searchTerm">The search term to use.</param>
        /// <returns>The Search view.</returns>
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction("Index");
            }

            IEnumerable<Brand> brands = await _brandRepository.Search(searchTerm);
            var model = (brands, searchTerm);
            return View(model);
        }
    }
}

