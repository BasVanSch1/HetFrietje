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
            var categories = await dbContext.Categories.ToListAsync();
            var products = await dbContext.Products.ToListAsync();

            var viewModel = new ProductListViewModel
            {
                Products = products,
                Categories = categories
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
