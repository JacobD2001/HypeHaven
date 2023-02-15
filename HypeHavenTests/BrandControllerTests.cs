using FakeItEasy;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//you can't unit test static functions httpcontext is static
namespace HypeHavenTests
{
    public class BrandControllerTests
    {
        private BrandController _brandController;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;

        //injecting these so values are for all functions in test, and you don't have to put them in every single one
        public BrandControllerTests()
        {
            //dependencies
            _brandRepository = A.Fake<IBrandRepository>();
            _categoryRepository = A.Fake<ICategoryRepository>();
            _productRepository = A.Fake<IProductRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
            //SUT - what we will be executing on (brandcontroller here) System Under Test
            _brandController = new BrandController(_brandRepository, _categoryRepository, _httpContextAccessor, _productRepository, _photoService);
        }

        [Fact] //it indicates that this is xUnit test
        public async void BrandController_Index_ReturnsSuccess() //whatcontroller_whataction_whatreturns(desired output)
        {
            //Arrange = what do I need to bring in?
            var brands = A.Fake<IEnumerable<Brand>>(); // we need to return IEnumerable<Brand> so we need to mock this, to fake this function
            A.CallTo(() => _brandRepository.GetAll()).Returns(brands); //fake call to getall method
            //Act
            var result = await _brandController.Index(); //calling an action of controller(simulation of the actual execution)
            //Assert( on this step you verify if the code produced expected result)
            var viewResult = Assert.IsType<ViewResult>(result);//verifies that the result returned by the index action is of the correct type
            //Assert.Equal("Index", viewResult.ViewName); //checks if viewName of viewresult is index action, so if we are calling the index action method
            //Assert.Equal(brands, viewResult.Model); // checks that the Model property of the ViewResult is set to the "brands" variable that was set up earlier in the test. This ensures that the data being displayed in the view is correct and matches the fake data that was set up for the test.
        }  
        [Fact] //it indicates that this is xUnit test
        public async void BrandController_MyBrandIndex_ReturnsSuccess() //whatcontroller_whataction_whatreturns(desired output)
        {
            //Arrange 
            var brands = A.Fake<IEnumerable<Brand>>();
            A.CallTo(() => _brandRepository.GetAllForSpecifedUser()).Returns(brands); //fake call to getall method
            //Act
            var result = await _brandController.MyBrandIndex(); //calling an action of controller(simulation of the actual execution)
            //Assert( on this step you verify if the code produced expected result)
            var viewResult = Assert.IsType<ViewResult>(result);//verifies that the result returned by the index action is of the correct type
        }
    }



}