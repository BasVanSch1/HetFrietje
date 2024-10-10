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
                            .Where(o => o.Status != OrderStatus.UNSENT) // Geen controle op de gebruiker omdat ik dit niet meer haal voor de deadline.
                            .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no order id supplied.";
                return RedirectToAction(nameof(Index));
            }

            var order = await dbContext.Orders
                        .Include(o => o.Products)
                        .ThenInclude(op => op.Product)
                        .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }


        public async Task<IActionResult> Cart()
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");

            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction("Index", "Home");
            }

            var order = await dbContext.Orders
                            .Include(o => o.Products)
                            .ThenInclude(po => po.Product)
                            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: cart content not found.";
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");

            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var order = await dbContext.Orders
                            .Include(o => o.Products)
                            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Cart));
            }

            var totalProductCount = 0;
            foreach (var product in order.Products)
            {
                totalProductCount += product.ProductCount;
            }

            TempData["MessageType"] = "confirmation";
            TempData["Message"] = $"Weet u zeker dat u deze order wil plaatsen? Totale prijs: {order.TotalPrice} Aantal producten: {totalProductCount}";
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> PlaceOrderConfirmed()
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");

            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Cart));
            }

            order.Status = OrderStatus.PLACED;
            await dbContext.SaveChangesAsync();

            TempData["MessageType"] = "success";
            TempData["Message"] = $"Uw bestelling is geplaatst. Uw nummer is {order.OrderId}. Bedankt voor uw bestelling!";
            return RedirectToAction(nameof(Cart));
        }


        public async Task<IActionResult> DeleteProductFromCart(int? productId)
        {
            if (productId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: product not provided.";
                return RedirectToAction(nameof(Cart));
            }

            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var productOrderCount = await dbContext.ProductOrders
                                        .Where(po => po.OrderId == orderId)
                                        .CountAsync();

            if (productOrderCount <= 1) // if there are no products in the order after this then delete the order entirely. the ProductOrders are cascade deleted.
            {
                var order = await dbContext.Orders.FindAsync(orderId);

                if (order == null)
                {
                    TempData["MessageType"] = "error";
                    TempData["Message"] = "Error: order not found.";
                    return RedirectToAction(nameof(Cart));
                }

                dbContext.Orders.Remove(order);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            var productOrder = await dbContext.ProductOrders.FindAsync(orderId, productId);
            if (productOrder == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product associated with this order found.";
                return RedirectToAction(nameof(Cart));
            }

            dbContext.ProductOrders.Remove(productOrder);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));

        }

        public async Task<IActionResult> IncreaseSingleItemFromOrder(int? productId)
        {
            if (productId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: product not provided.";
                return RedirectToAction(nameof(Cart));
            }

            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var productOrder = await dbContext.ProductOrders.FindAsync(orderId, productId);
            if (productOrder == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product associated with this order found.";
                return RedirectToAction(nameof(Cart));
            }

            productOrder.ProductCount++;
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> RemoveSingleItemFromOrder(int? productId)
        {
            if (productId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: product not provided.";
                return RedirectToAction(nameof(Cart));
            }

            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var productOrder = await dbContext.ProductOrders.FindAsync(orderId, productId);
            if (productOrder == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product associated with this order found.";
                return RedirectToAction(nameof(Cart));
            }

            if (productOrder.ProductCount <= 1)
            {
                return RedirectToAction(nameof(DeleteProductFromCart), new { productId });
            }

            productOrder.ProductCount--;

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> CancelOrder()
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            TempData["MessageType"] = "cancelOrderConfirmation";
            TempData["Message"] = "Weet u zeker dat u deze bestelling wilt annuleren?";
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> CancelOrderConfirmed()
        {
            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: session not found.";
                return RedirectToAction(nameof(Cart));
            }

            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Cart));
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RepeatOrder(int? orderId)
        {
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: orderid not supplied.";
                return RedirectToAction(nameof(Cart));
            }

            var order = await dbContext.Orders
                            .Include(o => o.Products)
                            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Cart));
            }

            var totalProductCount = 0;
            foreach (var product in order.Products)
            {
                totalProductCount += product.ProductCount;
            }

            TempData["MessageType"] = "repeatOrderConfirmation";
            TempData["Message"] = $"Weet u zeker dat u deze bestelling opnieuw wil plaatsen? Totale prijs: {order.TotalPrice} Aantal producten: {totalProductCount}";

            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        public async Task<IActionResult> RepeatOrderConfirmed(int? orderId)
        {
            if (orderId == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: orderid not supplied.";
                return RedirectToAction(nameof(Cart));
            }

            var order = await dbContext.Orders
                            .Include(o => o.Products)
                            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: order not found.";
                return RedirectToAction(nameof(Cart));
            }

            var newOrder = new Order()
            {
                Username = order.Username,
                Status = OrderStatus.PLACED,
                TotalPrice = order.TotalPrice,
                SubtotalPrice = order.SubtotalPrice,
                OrderDate = DateTime.Now
            };


            newOrder.Products = order.Products
                                    .Select(po => new ProductOrder
                                    {
                                        ProductId = po.ProductId,
                                        Order = newOrder,
                                        ProductCount = po.ProductCount
                                    }).ToList();
                                        
            dbContext.Orders.Add(newOrder);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProcesOrders()
        {
            return View();
        }

        public IActionResult FinishedOrders()
        {
            return View();
        }
    }
}
