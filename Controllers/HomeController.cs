using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

                public IActionResult About()
        {
            return View();
        }

    }
}
