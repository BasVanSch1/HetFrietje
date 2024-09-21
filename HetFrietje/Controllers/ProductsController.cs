using Microsoft.AspNetCore.Mvc;

namespace HetFrietje.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
