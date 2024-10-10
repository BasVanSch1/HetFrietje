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
            var products = await dbContext.Products
                                .Include(p => p.Categories)    
                                .ToListAsync();


            var orderId = HttpContext.Session.GetInt32("OrderId");
            int orderProductCount = 0;

            if (orderId != null)
            {
                var productOrders = await dbContext.ProductOrders
                                            .Where(po => po.OrderId == orderId)
                                            .ToListAsync();

                foreach (var product in productOrders)
                {
                    orderProductCount += product.ProductCount;
                }
            }

            var viewModel = new ProductListViewModel
            {
                Products = products,
                Categories = categories,
                OrderProductCount = orderProductCount 
            };

            return View(viewModel);
        }

        public IActionResult About()
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
