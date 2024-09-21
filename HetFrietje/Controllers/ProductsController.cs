﻿using HetFrietje.Data;
using HetFrietje.Models;
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product ID supplied.";
                return RedirectToAction(nameof(StockManagement));
            }

            var product = await dbContext.Products
                                    .Include(p => p.Categories)
                                    .Include(p => p.Options)
                                    .FirstOrDefaultAsync(p => p.ProductId == id); // Geen .FindAsync() mogelijk hier omdat .Include() geen DbSet returnt maar een IQueryable en die heeft geen .FindAsync() method

            if (product == null)
            {
                TempData["MessageType"] = "error";
                TempData["Messsage"] = "Error: product does not exist";
                return RedirectToAction(nameof(StockManagement));
            }

            var categories = await dbContext.Categories.ToListAsync();
            var productOptions = await dbContext.Options.ToListAsync();
            Tuple<Product, List<Category>, List<Option>> data = new(product, categories, productOptions);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductId", "Name", "Description", "Price", "SalesPrice", "Tax", "Stock", "Options", "Categories", Prefix = "Item1" )] Product product)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Error: no product ID supplied.";
                return RedirectToAction(nameof(Edit));
            }

            try
            {
                dbContext.Update(product);
                await dbContext.SaveChangesAsync();
            } catch
            {
                throw;
            }

            return RedirectToAction(nameof(StockManagement));
        }
    }
}
