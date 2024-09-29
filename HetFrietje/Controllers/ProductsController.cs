﻿using HetFrietje.Data;
using HetFrietje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = product,
                Categories = categories,
                Options = productOptions,

                SelectedCategoryIds = product.Categories.Select(c => c.CategoryId).ToList(),
                SelectedOptionsIds = product.Options.Select(o => o.OptionId).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Failed to edit product. ModelState is invalid.";
                return RedirectToAction(nameof(StockManagement));
            }

            var existingProduct = await dbContext.Products
                                .Include(p => p.Categories)
                                .Include(p => p.Options)
                                .FirstOrDefaultAsync(p => p.ProductId == model.Product.ProductId); // get product from database instead of memory.


            if (existingProduct == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "ERROR: Product does not exist in the database!";
                return RedirectToAction(nameof(StockManagement));
            }


            if (model.SelectedCategoryIds != null)
            {
                if (existingProduct.Categories == null)
                {
                    existingProduct.Categories = []; // initialize new list.
                }

                existingProduct.Categories.Clear();

                foreach (var selectedCategoryId in model.SelectedCategoryIds)
                {
                    var category = dbContext.Categories.FirstOrDefault(c => c.CategoryId == selectedCategoryId);

                    if (category != null)
                    {
                        existingProduct.Categories.Add(category);
                    }
                }
            }

            if (model.SelectedOptionsIds != null)
            {
                if (existingProduct.Options == null)
                {
                    existingProduct.Options = [];
                }

                existingProduct.Options.Clear();

                foreach (var selectedOptionId in model.SelectedOptionsIds)
                {
                    var option = dbContext.Options.FirstOrDefault(o => o.OptionId == selectedOptionId);

                    if (option != null)
                    {
                        existingProduct.Options.Add(option);
                    }
                }
            }
            

            dbContext.Entry(existingProduct).CurrentValues.SetValues(model.Product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(StockManagement));


        }

        public async Task<IActionResult> Create()
        {
            var categories = await dbContext.Categories.ToListAsync();
            var productOptions = await dbContext.Options.ToListAsync();
            var newestProduct = await dbContext.Products
                                            .OrderBy(p => p.ProductId) // sorteer de lijst op ProductId (van laag naar hoog)
                                            .LastAsync(); // haal het laatste id op

            int _productId = 1;

            if (newestProduct != null)
            {
                _productId = newestProduct.ProductId + 1; // Incrementeer het productId met 1 voor een nieuwe productId
            }
            var emptyProduct = new Product()
            {
                ProductId = _productId
            };

            Tuple<Product, ICollection<Category>, ICollection<Option>> data = new(emptyProduct, categories, productOptions);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Description", "Price", "SalesPrice", "Tax", "Stock", "Options", "Categories", Prefix = "Product")] Product product)
        {
            
            if (ModelState.IsValid)
            {

                dbContext.Add(product);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(StockManagement));
            }


            TempData["MessageType"] = "error";
            TempData["Message"] = "Failed to create product";
            return RedirectToAction(nameof(StockManagement));
        }
    }
}
