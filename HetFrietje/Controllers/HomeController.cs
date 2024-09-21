using HetFrietje.Data;
using HetFrietje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HetFrietje.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext dbContext;

        public HomeController(DatabaseContext _context)
        {
            dbContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var productCategories = await dbContext.ProductCategory.Include(p => p.Category).Include(p => p.Product).ToListAsync();
            var categories = await dbContext.Categories.ToListAsync();
            Tuple<List<ProductCategory>, List<Category>> dbData = new(productCategories, categories);

            return View(dbData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
