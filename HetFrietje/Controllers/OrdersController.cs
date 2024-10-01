using HetFrietje.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HetFrietje.Controllers
{
    public class OrdersController : Controller
    {

        private readonly DatabaseContext dbContext;

        public OrdersController(DatabaseContext _context)
        {
            dbContext = _context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await dbContext.Orders
                            .Include(o => o.Products)
                            .ToListAsync();

            return View(orders);
        }
    }
}
