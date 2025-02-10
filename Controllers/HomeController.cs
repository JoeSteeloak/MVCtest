using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mvc.Models

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

        [Route("/meny")]
        public IActionResult Menu()
        {
            var jsonStr = System.IO.File.ReadAllText("menu.json");
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<Menu>>(jsonStr);
            return View();
        }

        [Route("/omoss")]
        public IActionResult About()
        {
            return View();
        }

    }
}
