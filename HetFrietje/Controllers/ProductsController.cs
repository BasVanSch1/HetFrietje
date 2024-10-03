using HetFrietje.Data;
using HetFrietje.Models;
using Microsoft.AspNetCore.Http;
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
            var selectedCategoryIds = new List<int>();
            var selectedOptionsIds = new List<int>();

            if (product.Categories != null)
            {
                selectedCategoryIds = product.Categories.Select(c => c.CategoryId).ToList(); // from product.Categories select all the categoryIds and put them in a list.
            }

            if (product.Options != null)
            {
                selectedOptionsIds = product.Options.Select(o => o.OptionId).ToList(); // same as above
            }

            ProductViewModel viewModel = new ProductViewModel
            {
                Product = product,
                Categories = categories,
                Options = productOptions,

                SelectedCategoryIds = selectedCategoryIds,
                SelectedOptionsIds = selectedOptionsIds
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


            if (model.SelectedCategoryIds != null) // This is almost always true, but just in case.
            {
                if (existingProduct.Categories == null)
                {
                    existingProduct.Categories = []; // initialize new list.
                }

                existingProduct.Categories.Clear(); // clear the list (whether it exists or not.

                foreach (var selectedCategoryId in model.SelectedCategoryIds) // go through all selected category ids
                {
                    var category = dbContext.Categories.FirstOrDefault(c => c.CategoryId == selectedCategoryId); // get the category from the database

                    if (category != null) // do a check in case the category is not found
                    {
                        existingProduct.Categories.Add(category); // add to the existingProduct's Category list in the dbContext so that the State changes to Modified.
                    }
                }

                if (model.Product.SalesPrice != null) // Add the 'Aanbiedingen' category when a SalesPrice is set.
                {
                    var category = dbContext.Categories.FirstOrDefault(c => c.Name.Equals("Aanbiedingen"));

                    if (category != null)
                    {
                        existingProduct.Categories.Add(category);
                    }
                }
            }

            if (model.SelectedOptionsIds != null) // same as above but then for Options.
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


            dbContext.Entry(existingProduct).CurrentValues.SetValues(model.Product); // update scalar values (int, string etc) 
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

            ProductViewModel viewModel = new ProductViewModel
            {
                Product = emptyProduct,
                Categories = categories,
                Options = productOptions,

                SelectedCategoryIds = [],
                SelectedOptionsIds = []
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Failed to create product. ModelState is invalid.";
                return RedirectToAction(nameof(StockManagement));
            }

            if (model.SelectedCategoryIds != null) // same as in the Edit method.
            {
                if (model.Product.Categories == null)
                {
                    model.Product.Categories = [];
                }

                model.Product.Categories.Clear();

                foreach (var selectedCategoryId in model.SelectedCategoryIds)
                {
                    var category = dbContext.Categories.FirstOrDefault(c => c.CategoryId == selectedCategoryId);

                    if (category != null)
                    {
                        model.Product.Categories.Add(category);
                    }
                }
            }

            if (model.SelectedOptionsIds != null)  // same as in the Edit method.
            {
                if (model.Product.Options == null)
                {
                    model.Product.Options = [];
                }

                model.Product.Options.Clear();

                foreach (var selectedOptionId in model.SelectedOptionsIds)
                {
                    var option = dbContext.Options.FirstOrDefault(o => o.OptionId == selectedOptionId);

                    if (option != null)
                    {
                        model.Product.Options.Add(option);
                    }
                }
            }

            dbContext.Add(model.Product);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(StockManagement));
        }


        public async Task<IActionResult> CustomizeProduct(int? id)
        {
            if (id == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "No productid has been provided.";

                return RedirectToAction(nameof(Index), "Home", new { area = "" }); // redirect to ("action", "ControllerName", routevalues (is verplicht) )
            }

            var product = await dbContext.Products
                                .Include(p => p.Categories)
                                .Include(p => p.Options)
                                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                TempData["MessageType"] = "error";
                TempData["Message"] = "Product not found.";

                return RedirectToAction(nameof(Index), "Home", new { area = "" });
            }

            var order = await dbContext.Orders
                            .FirstOrDefaultAsync(o => o.Status == OrderStatus.UNSENT); // voor nu geen username checks omdat dit teveel tijd vereist.

            if (order == null) 
            {
                order = new Order();
            }

            var productOrder = new ProductOrder()
            {
                ProductId = product.ProductId,
                Product = product,
                Order = order,
                OrderId = order.OrderId,
                ProductCount = 0
            };

            // get categories & options from the databse for the viewmodel
            var categories = await dbContext.Categories.ToListAsync();
            var options = await dbContext.Options.ToListAsync();

            var viewModel = new ProductOrderViewModel()
            {
                ProductOrder = productOrder,
                Categories = categories,
                Options = options
            };

            return View(viewModel);
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
                await dbContext.SaveChangesAsync();

                TempData["MessageType"] = "success_addedproduct";
                TempData["Message"] = $"{model.ProductOrder.ProductCount}x {model.ProductOrder.Product.Name} is toegevoegd aan uw winkelwagen";
            } else
            {
                // ==add selected productoptions==              (dit is nog niet werkend)

                dbContext.Add(model.ProductOrder);
                await dbContext.SaveChangesAsync();

                TempData["MessageType"] = "success_addedproduct";
                TempData["Message"] = $"{model.ProductOrder.ProductCount}x {model.ProductOrder.Product.Name} is toegevoegd aan uw winkelwagen";
            }

            HttpContext.Session.SetInt32("OrderId", model.ProductOrder.OrderId); // zet de session variable.
            return RedirectToAction(nameof(Index), "Home", new { area = "" });
        }
    }
}
