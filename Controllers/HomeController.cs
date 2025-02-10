using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mvc.Models;

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
            return View(JsonObj);
        }

        [Route("/omoss")]
        public IActionResult About()
        {
            var model = new ContactFormModel();  // Skapa en instans av modellen
    return View(model);  // Skicka modellen till vyn
        }

        // POST: Home/Contact
        [HttpPost]
        [Route("/omoss")]
        public IActionResult About(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                ViewData["Message"] = "Ditt meddelande har skickats!";
                return View();
            }
            return View(model);
        }

    }
}
