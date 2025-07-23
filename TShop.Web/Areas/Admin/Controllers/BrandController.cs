using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TShop.Application.DTOs;
using TShop.Application.Interfaces.Sevices;
using TShop.Web.ViewModels;
using Microsoft.Extensions.Logging;

namespace TShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService brandService, IMapper mapper, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("BrandController - Index called.");
            var brands = await _brandService.GetAllAsync();
            var model = _mapper.Map<List<BrandVM>>(brands);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("BrandController - Details called with ID: {Id}", id);

            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                _logger.LogWarning("Brand not found with ID: {Id}", id);
                TempData["error"] = "Brand not found!";
                return RedirectToAction(nameof(Index));
            }

            var model = _mapper.Map<BrandVM>(brand);
            return View(model);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("BrandController - Create GET called.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandVM vm)
        {
            _logger.LogInformation("BrandController - Create POST called with Name: {Name}", vm.Name);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create model state invalid.");
                TempData["warning"] = "Please correct the form errors!";
                return View(vm);
            }

            var dto = _mapper.Map<BrandDto>(vm);
            await _brandService.CreateAsync(dto);

            _logger.LogInformation("Brand created successfully with Name: {Name}", vm.Name);
            TempData["success"] = "Brand created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("BrandController - Edit GET called with ID: {Id}", id);

            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                _logger.LogWarning("Brand not found for edit with ID: {Id}", id);
                TempData["error"] = "Brand not found!";
                return RedirectToAction(nameof(Index));
            }

            var vm = _mapper.Map<BrandVM>(brand);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandVM vm)
        {
            _logger.LogInformation("BrandController - Edit POST called for ID: {Id}", id);

            if (id != vm.Id)
            {
                _logger.LogError("Edit ID mismatch. URL ID: {UrlId}, ViewModel ID: {VmId}", id, vm.Id);
                TempData["error"] = "ID mismatch!";
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit model state invalid.");
                TempData["warning"] = "Please correct the form errors!";
                return View(vm);
            }

            var dto = _mapper.Map<BrandDto>(vm);
            var updated = await _brandService.UpdateAsync(dto);

            if (updated == null)
            {
                _logger.LogWarning("Brand not found to update with ID: {Id}", id);
                TempData["error"] = "Brand not found to update!";
                return RedirectToAction(nameof(Index));
            }

            _logger.LogInformation("Brand updated successfully with ID: {Id}", id);
            TempData["success"] = "Brand updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("BrandController - Delete GET called with ID: {Id}", id);

            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
            {
                _logger.LogWarning("Brand not found to delete with ID: {Id}", id);
                TempData["error"] = "Brand not found!";
                return RedirectToAction(nameof(Index));
            }

            var vm = _mapper.Map<BrandVM>(brand);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("BrandController - DeleteConfirmed POST called with ID: {Id}", id);
            var deleted = await _brandService.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Failed to delete Brand with ID: {Id}", id);
                TempData["error"] = "Failed to delete brand!";
                return RedirectToAction(nameof(Index));
            }

            _logger.LogInformation("Brand deleted successfully with ID: {Id}", id);
            TempData["success"] = "Brand deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
