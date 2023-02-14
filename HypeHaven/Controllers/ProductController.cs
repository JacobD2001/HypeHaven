﻿using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.Repositories;
using HypeHaven.ViewModels;
using HypeHaven.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing.Drawing2D;


namespace HypeHaven.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBrandRepository _brandRepository;
        private readonly IPhotoService _photoService;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, IBrandRepository brandRepository, IPhotoService photoService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _brandRepository = brandRepository;
            _photoService = photoService;
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
                var result = await _photoService.AddPhotoAsync(productVM.Image);


                var product = new Product
                {
                    Name = productVM.Name,
                    Description = productVM.Description,
                    Price = productVM.Price,
                    Image = result.Url.ToString(),
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
            else if (!ModelState.IsValid)
            {
                productVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                productVM.Brands = (List<Brand>)await _brandRepository.GetAll();
                return View(productVM);
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


            var productViewModel = new EditProductViewModel
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
  
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            //as no tracking
            var curProduct = await _productRepository.GetByIdAsyncNoTracking(id);
            var result = await _photoService.AddPhotoAsync(productVM.Image);


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
            else if (!ModelState.IsValid)
            {
                productVM.Categories = (List<Category>)await _categoryRepository.GetAll();
                productVM.Brands = (List<Brand>)await _brandRepository.GetAll();
                return View(productVM);
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

    }
}
