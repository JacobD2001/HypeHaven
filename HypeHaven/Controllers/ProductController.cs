﻿using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.Repositories;
using HypeHaven.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;


namespace HypeHaven.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBrandRepository _brandRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

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
                Brands = (List<Brand>)await _brandRepository.GetAll()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                var brand = await _brandRepository.GetByIdAsync(productVM.BrandId);
                var category = await _categoryRepository.GetByIdAsync(productVM.CategoryId);
     

                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = productVM.Image,
                    Size = productVM.Size,
                    Color = productVM.Color,
                    Material = productVM.Material,
                    Quantity = productVM.Quantity,
                    CategoryId = category.CategoryId,
                    BrandId = brand.BrandId
                };
               
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");


            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var brand = await _brandRepository.GetByIdAsync(product.BrandId);


            var productViewModel = new CreateProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
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
  
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateProductViewModel productVM)
        {
            //as no tracking
            var curProduct = await _productRepository.GetByIdAsyncNoTracking(id);

            if (curProduct == null)
                return NotFound();


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
                    Image = productVM.Image,
                    Size = productVM.Size,
                    Color = productVM.Color,
                    Material = productVM.Material,
                    Quantity = productVM.Quantity,
                    CategoryId = category.CategoryId,
                    BrandId = brand.BrandId
                };

                _productRepository.Update(product);
                return RedirectToAction("Index");
            }

            return View(productVM);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");


            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            var brand = await _brandRepository.GetByIdAsync(product.BrandId);


            var productViewModel = new CreateProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            _productRepository.Delete(product);
            return RedirectToAction("Index");
        }

    }
}