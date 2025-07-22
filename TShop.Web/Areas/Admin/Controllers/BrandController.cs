using Microsoft.AspNetCore.Mvc;
using TShop.Web.ViewModels;

namespace TShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
       
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
                Console.WriteLine($"Brand Created: {model.Name}");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
