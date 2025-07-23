using Microsoft.AspNetCore.Mvc;
using TShop.Web.ViewModels;

namespace TShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;
        public BrandController(ILogger<BrandController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BrandVM model)
        {
            if (ModelState.IsValid)
            {
                // Logic to add the brand using a service or repository
                // await _brandService.AddAsync(model);
                _logger.LogInformation("Brand Created: {BrandName}", model.Name);
                return RedirectToAction("Index");
            }
            _logger.LogWarning("Brand Create POST request failed. Invalid model state.");
            return View(model);
        }
    }
}
