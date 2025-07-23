using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TShop.Application.DTOs;
using TShop.Application.Interfaces.Sevices;

using TShop.Web.ViewModels;

namespace TShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {

        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        // GET: Brand
        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var model = _mapper.Map<List<BrandVM>>(brands);
            return View(model);
        }

        // GET: Brand/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var model = _mapper.Map<BrandVM>(brand);
            return View(model);
        }

        // GET: Brand/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<BrandDto>(vm);
            await _brandService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var vm = _mapper.Map<BrandVM>(brand);
            return View(vm);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandVM vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);

            var dto = _mapper.Map<BrandDto>(vm);
            var updated = await _brandService.UpdateAsync(dto);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: Brand/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var vm = _mapper.Map<BrandVM>(brand);
            return View(vm);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _brandService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
