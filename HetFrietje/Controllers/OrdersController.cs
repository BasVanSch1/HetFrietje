using HetFrietje.Data;
using HetFrietje.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductToOrder(ProductOrderViewModel model)
        {
            var existingProductOrder = await dbContext.ProductOrders
                                            .FirstOrDefaultAsync(po => po.ProductId == model.ProductOrder.ProductId && po.OrderId == model.ProductOrder.OrderId);

            var existingProduct = await dbContext.Products
                                        .FirstOrDefaultAsync(p => p.ProductId == model.ProductOrder.ProductId); // get product from database so it can be added to the model.

            if (existingProduct == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Product not found.";

                return RedirectToAction(nameof(Index), "Home", new { area = "" });
            }

            var order = await dbContext.Orders
                                .FirstOrDefaultAsync(o => o.OrderId == model.ProductOrder.OrderId && o.Status == OrderStatus.UNSENT); // extra check to see if the order has the right status. this is not really neccesary.

            if (order == null)
            {
                order = new Order();
            }

            // add references to the right objects to model again, since these were not sent when submitting the form.
            model.ProductOrder.Product = existingProduct;
            model.ProductOrder.Order = order;

            if (existingProductOrder != null) // in case the product was already added to the same order before.
            {
                // ==add selected productoptions==              (dit is nog niet werkend)

                existingProductOrder.ProductCount += model.ProductOrder.ProductCount;

                TempData["MessageType"] = "success_addedproduct";
                TempData["Message"] = $"{model.ProductOrder.ProductCount}x {model.ProductOrder.Product.Name} is toegevoegd aan uw winkelwagen";
            }
            else
            {
                // ==add selected productoptions==              (dit is nog niet werkend)

                dbContext.Add(model.ProductOrder);

                TempData["MessageType"] = "success_addedproduct";
                TempData["Message"] = $"{model.ProductOrder.ProductCount}x {model.ProductOrder.Product.Name} is toegevoegd aan uw winkelwagen";
            }

            model.ProductOrder.Order.CalculateTotalPrice();
            await dbContext.SaveChangesAsync();

            HttpContext.Session.SetInt32("OrderId", model.ProductOrder.OrderId); // zet de session variable.
            return RedirectToAction(nameof(Index), "Home", new { area = "" });
        }
    }
}
