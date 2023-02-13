using HypeHaven.Areas.Identity.Data;
using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.ViewModels;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;


        public BrandController(IBrandRepository brandRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //controler 
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAll(); //model
            return View(brands); //view
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            Brand brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            return View(brand);
        }
 
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandViewModel brandVM)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(brandVM.CategoryId);

                var brand = new Brand
                {
                    Name= brandVM.Name,
                    Description = brandVM.Description,
                    Location= brandVM.Location,
                    Image = brandVM.Location,
                    Email = brandVM.Email,
                    PhoneNumber = brandVM.PhoneNumber,
                    Instagram = brandVM.Instagram,
                    Facebook = brandVM.Facebook,
                    Pinterest = brandVM.Pinterest,
                    Tiktok = brandVM.Tiktok,
                    Video = brandVM.Video,
                    CategoryId = category.CategoryId,
                    Id = currentUserId
                    
                };
                _brandRepository.Add(brand);
                return RedirectToAction("Index");
            }
            return View(brandVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) return View("Error");
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var category = await _categoryRepository.GetByIdAsync(brand.CategoryId);

            var brandVM = new CreateBrandViewModel
            {
                Name = brand.Name,
                Description = brand.Description,
                Location = brand.Location,
                Image = brand.Location,
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateBrandViewModel brandVM)
        {
            var curBrand = await _brandRepository.GetByIdAsyncNoTracking(id);
            if (curBrand == null) return View("Error");

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsyncNoTracking(brandVM.CategoryId);

                curBrand.Name = brandVM.Name;
                curBrand.Description = brandVM.Description;
                curBrand.Location = brandVM.Location;
                curBrand.Image = brandVM.Image;
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
            return View(brandVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) return View("Error");

            var category = await _categoryRepository.GetByIdAsync(brand.CategoryId);
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            var brandVM = new CreateBrandViewModel
            {
                Name = brand.Name,
                Description = brand.Description,
                Location = brand.Location,
                Image = brand.Location,
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            _brandRepository.Delete(brand);
            return RedirectToAction("Index");
        }
    }
}
