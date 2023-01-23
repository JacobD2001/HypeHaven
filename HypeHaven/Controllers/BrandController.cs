using HypeHaven.Areas.Identity.Data;
using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;


namespace HypeHaven.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BrandController(IBrandRepository brandRepository, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //controler 
        {
            IEnumerable<Brand> brands = await _brandRepository.GetAll(); //model
            return View(brands); //view
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Brand brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            return View(brand);
        }
 
        //todo - zamiast przez viewbag to dodać viewmodele
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var brandModel = new Brand { Id = currentUserId }; // brand.Id = currentUserId;
            ViewBag.Categories = await _categoryRepository.GetAll();
            return View(brandModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {                     
            if (ModelState.IsValid)
            {
                _brandRepository.Add(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Update(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return NotFound();
            return View(brand);
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
