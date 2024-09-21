using HetFrietje.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HetFrietje.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext dbContext;

        public ProductsController(DatabaseContext _context)
        {
            dbContext = _context;
        }

        public async Task<IActionResult> StockManagement()
        {
            var products = await dbContext.Products.ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Messsage"] = "Error: no product ID supplied.";
                return RedirectToAction(nameof(StockManagement));
            }

            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                TempData["MessageType"] = "error";
                TempData["Messsage"] = "Error: product does not exist";
                return RedirectToAction(nameof(StockManagement));
            }

            TempData["MessageType"] = "confirmation";
            TempData["Message"] = $"Weet u zeker dat u artikel {product.ProductId} - {product.Name} wilt verwijderen uit het assortiment?";
            TempData["ProductId"] = product.ProductId;
            return RedirectToAction(nameof(StockManagement));
        }

        public async Task<IActionResult> DeleteProductConfirmed(int? id)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product ID supplied.";
                return RedirectToAction(nameof(StockManagement));
            }

            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                TempData["MessageType"] = "error";
                TempData["Messsage"] = "Error: product does not exist";
                return RedirectToAction(nameof(StockManagement));
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(StockManagement));
        }
    }
}
