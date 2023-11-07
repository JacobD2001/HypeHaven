using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.Repositories;
using HypeHaven.ViewModels;
using HypeHaven.ViewModels.Helpers;
using HypeHaven.ViewModels.ProductViewModels;
using HypeHaven.ViewModels.ReviewViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Drawing2D;


namespace HypeHaven.Controllers
{
    /// <summary>
    /// Controller for managing products.
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBrandRepository _brandRepository;
        private readonly IPhotoService _photoService;
        private readonly IFavoriteProductRepository _favoriteProductRepository;
        private readonly IReviewRepository _reviewRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="brandRepository">The brand repository.</param>
        /// <param name="photoService">The photo service.</param>
        /// <param name="favoriteProductRepository">The favorite product repository.</param>
        /// <param name="reviewRepository">The review repository.</param>
        public ProductController(IProductRepository productRepository, IRepository<Category> categoryRepository, IHttpContextAccessor httpContextAccessor, IBrandRepository brandRepository, IPhotoService photoService, IFavoriteProductRepository favoriteProductRepository, IReviewRepository reviewRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
            _photoService = photoService;
            _favoriteProductRepository = favoriteProductRepository;
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Returns the view for the Index page.
        /// </summary>
        /// <param name="priceSortOrder">The sort order for the price.</param>
        /// <param name="categoryFilter">The ID of the category to filter by.</param>
        /// <param name="searchTerm">The search term to filter by.</param>
        /// <returns>The Index view.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string priceSortOrder, int? categoryFilter, string searchTerm)
        {
            var products = await _productRepository.GetAll();
            var categories = await _categoryRepository.GetAll(); // Replace with your actual category retrieval logic.

            if (!string.IsNullOrEmpty(priceSortOrder))
            {
                // Apply price sorting
                products = await _productRepository.SortProductsByPrice(products, priceSortOrder);
            }

            if (categoryFilter.HasValue)
            {
                // Apply category filtering
                products = await _productRepository.FilterProductsByCategory(products, categoryFilter.Value);
            }

            var viewModel = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryFilter,
                PriceSortOrder = priceSortOrder,
                SearchTerm = searchTerm
            };

            if (!string.IsNullOrWhiteSpace(searchTerm))
                return RedirectToAction("Search", "Product", new { searchTerm, categoryFilter, priceSortOrder }); 
    
                return View(viewModel);
        }

        /// <summary>
        /// Returns the view for the FavoriteIndex page.
        /// </summary>
        /// <returns>The FavoriteIndex view.</returns>
        [HttpGet]
        public async Task<IActionResult> FavoriteIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                IEnumerable<Product> favoriteProducts = await _productRepository.GetFavoriteProducts(currentUserId);
                return View(favoriteProducts);
            }
            else
            {
                // Handle the case where the user is not authenticated 
                return View("NotAuthenticated"); 
            }
        }

        /// <summary>
        /// Returns the view for the MyProductIndex page.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The MyProductIndex view.</returns>
        [HttpGet]
        public async Task<IActionResult> MyProductIndex(int id) //controler 
        {
            IEnumerable<Product> products = await _productRepository.GetAllForSpecifedBrand(id); //model
            return View(products); //view
        }

        /// <summary>
        /// Returns the view for the Detail page.
        /// </summary>
        /// <param name="id">The ID of the brand to display.</param>
        /// <returns>The Detail view.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            //return all reviews of this product
            IEnumerable<Review> reviews = await _productRepository.GetReviewsForSpecifedProduct(id);

            // Create and populate ProductDetailViewModel
            var productDetailViewModel = new ProductDetailViewModel
            {
                Product = product,
                Reviews = reviews
            };

            // Create and populate AddReviewViewModel
            var addReviewViewModel = new AddReviewViewModel
            {
                ProductId = product.ProductId, 
                BrandId = product.BrandId,
                Id = _httpContextAccessor.HttpContext.User.GetUserId(),               
            };

            var viewModel = new CompositeViewModel
            {
                ProductDetailViewModel = productDetailViewModel,
                AddReviewViewModel = addReviewViewModel,
                HttpContextAccessor= _httpContextAccessor
            };
      
            return View(viewModel);
        }

        /// <summary>
        /// Adds a review to the database.
        /// </summary>
        /// <param name="AddReviewVM">The AddReviewViewModel to add.</param>
        /// <returns>The Detail view.</returns>
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel AddReviewVM) 
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId(); //here take current user id
            AddReviewVM.Id = currentUserId; //set current user id to review
            

            if (ModelState.IsValid)
            {
                var brand = await _brandRepository.GetByIdAsync(AddReviewVM.BrandId);
                var product = await _productRepository.GetByIdAsync(AddReviewVM.ProductId);

                var review = new Review
                {
                    Text = AddReviewVM.Text,
                    Rating = AddReviewVM.Rating,
                    ProductId = product.ProductId,               
                    BrandId = brand.BrandId,
                    UserId = currentUserId,
                    Id = currentUserId
                };

                _reviewRepository.Add(review);
                return RedirectToAction("Detail", "Product", new { id = review.ProductId });
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the view for the Create page.
        /// </summary>
        /// <returns>The Create view.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //current user is adding a product
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var productModel = new Product { Brand = new Brand { Id = currentUserId } };
            var productViewModel = new CreateProductViewModel
            {
                BrandId = productModel.BrandId,
                Categories = (List<Category>)await _categoryRepository.GetAll(),
                Brands = (List<Brand>)await _brandRepository.GetAllForSpecifedUser()
            };
            return View(productViewModel);
        }

        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        /// <param name="productVM">The CreateProductViewModel to add.</param>
        /// <returns>The Index view.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (ModelState.IsValid)
            {
                var brand = await _brandRepository.GetByIdAsync(productVM.BrandId);
                var category = await _categoryRepository.GetByIdAsync(productVM.CategoryId);
                var result = await _photoService.AddPhotoAsync(productVM.Image);


                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = result.Url.ToString(),
                    Size = productVM.SelectedSize,
                    Color = productVM.Color,
                    Material = productVM.Material,
                    Quantity = productVM.Quantity,
                    CategoryId = category.CategoryId,
                    UserId = currentUserId,
                    BrandId = brand.BrandId
                };

                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                productVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                productVM.Brands = (List<Brand>)await _brandRepository.GetAll();
                return View(productVM);
            }
            return View(productVM);
        }

        /// <summary>
        /// Returns the view for the Edit page.
        /// </summary>
        /// <param name="id">The ID of the product to edit.</param>
        /// <returns>The Edit view.</returns>
        [HttpGet]
        [Authorize(Roles = "vendor")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");


            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var brand = await _brandRepository.GetByIdAsync(product.BrandId);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (product.UserId == currentUserId)
            {
                var productViewModel = new EditProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    URL = product.Image,
                    SelectedSize = product.Size,
                    Color = product.Color,
                    Material = product.Material,
                    Quantity = product.Quantity,
                    CategoryId = category.CategoryId,
                    Categories = (List<Category>)await _categoryRepository.GetAll(),
                    BrandId = brand.BrandId,
                    Brands = (List<Brand>)await _brandRepository.GetAll()

                };
                return View(productViewModel);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edits a product in the database.
        /// </summary>
        /// <param name="id">The ID of the product to edit.</param>
        /// <param name="productVM">The EditProductViewModel to edit.</param>
        /// <returns>The Index view.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            //as no tracking
            var curProduct = await _productRepository.GetByIdAsyncNoTracking(id);
            var result = await _photoService.AddPhotoAsync(productVM.Image);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (curProduct == null)
                return NotFound();
            if (!string.IsNullOrEmpty(curProduct.Image))
            {
                _ = _photoService.DeletePhotoAsync(curProduct.Image);
            }


            if (ModelState.IsValid)
            {
                var brand = await _brandRepository.GetByIdAsyncNoTracking(productVM.BrandId);
                var category = await _categoryRepository.GetByIdAsyncNoTracking(productVM.CategoryId);

                var product = new Product
                {
                    ProductId = id,
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = result.Url.ToString(),
                    Size = productVM.SelectedSize,
                    Color = productVM.Color,
                    Material = productVM.Material,
                    Quantity = productVM.Quantity,
                    CategoryId = category.CategoryId,
                    BrandId = brand.BrandId,
                    UserId = currentUserId
                };

                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            else if (!ModelState.IsValid)
            {
                productVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                productVM.Brands = (List<Brand>)await _brandRepository.GetAll();
                return View(productVM);
            }

            return View(productVM);
        }

        /// <summary>
        /// Returns the view for the Delete page.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The Delete view.</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");


            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var brand = await _brandRepository.GetByIdAsync(product.BrandId);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (product.UserId == currentUserId)
            {
                var productViewModel = new DeleteProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    URL = product.Image,
                    Size = product.Size,
                    Color = product.Color,
                    Material = product.Material,
                    Quantity = product.Quantity,
                    CategoryId = category.CategoryId,
                    Categories = (List<Category>)await _categoryRepository.GetAll(),
                    BrandId = brand.BrandId,
                    Brands = (List<Brand>)await _brandRepository.GetAll()

                };
                return View(productViewModel);

            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The Index view.</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            if (!string.IsNullOrEmpty(product.Image))
            {
                _ = _photoService.DeletePhotoAsync(product.Image);
            }
            _productRepository.Delete(product);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the view for the Search page.
        /// </summary>
        /// <param name="searchTerm">The search term to filter by.</param>
        /// <param name="categoryFilter">The ID of the category to filter by.</param>
        /// <param name="priceSortOrder">The sort order for the price.</param>
        /// <returns>The Index view.</returns>
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, int? categoryFilter, string priceSortOrder)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return RedirectToAction("Index");

            IEnumerable<Product> products = await _productRepository.Search(searchTerm);

            // Apply price sorting
            if (!string.IsNullOrEmpty(priceSortOrder))
                products = await _productRepository.SortProductsByPrice(products, priceSortOrder);


            // Apply category filtering
            if (categoryFilter.HasValue)
                products = await _productRepository.FilterProductsByCategory(products, categoryFilter.Value);

            var categories = await _categoryRepository.GetAll(); 


            var model = (products, searchTerm, categoryFilter, priceSortOrder, categories);
            return View(model);
        }

        /// <summary>
        /// Adds a product to the user's favorites.
        /// </summary>
        /// <param name="productId">The ID of the product to add.</param>
        /// <returns>The index view.</returns>
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (!_favoriteProductRepository.IsFavorite(currentUserId, productId))
            {
                var product = await _productRepository.AddToFavoritesAsync(productId);
            }


            return RedirectToAction("Index");
        }

        /// <summary>
        /// Removes a product from the user's favorites.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <returns>The index view.</returns>
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (_favoriteProductRepository.IsFavorite(currentUserId, productId))
            {
                var product = await _productRepository.RemoveFromFavoritesAsync(productId);
            }

            return RedirectToAction("Index");
        }






    }
}
