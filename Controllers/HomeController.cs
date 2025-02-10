using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Greeting = "Välkommen till vår Pizzeria!";
            ViewData["OpeningHours"] = "Vi har öppet alla dagar 11:00 - 23:00";
            ViewData["FavoritePizza"] = HttpContext.Session.GetString("FavoritePizza") ?? "Du har inte valt en favoritpizza ännu!";

            return View();
        }

        [HttpPost]
        public IActionResult SetFavoritePizza(string pizza)
        {
            // Spara favoritpizzan i sessionen
            HttpContext.Session.SetString("FavoritePizza", pizza);
            return RedirectToAction("Index");
        }

        public IActionResult ClearFavoritePizza()
        {
            // Ta bort favoritpizzan från sessionen
            HttpContext.Session.Remove("FavoritePizza");
            return RedirectToAction("Index");
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
            var model = new ContactFormModel(); 
            return View(model); 
        }

        // Metod för att skicka till JSON
        [HttpPost]
        [Route("/omoss/skicka")]
        public IActionResult SendToJson(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                string filePath = "contactMessages.json";
                List<ContactFormModel> messages = new();

                if (System.IO.File.Exists(filePath))
                {
                    string existingJson = System.IO.File.ReadAllText(filePath);
                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        messages = JsonConvert.DeserializeObject<List<ContactFormModel>>(existingJson) ?? new List<ContactFormModel>();
                    }
                }

                // Lägg till det nya meddelandet
                messages.Add(model);

                // Skriv tillbaka till filen
                string updatedJson = JsonConvert.SerializeObject(messages, Formatting.Indented);
                System.IO.File.WriteAllText(filePath, updatedJson);

                // Skicka bekräftelse och rensa formuläret
                TempData["SuccessMessage"] = "Ditt meddelande har skickats och sparats!";
                return RedirectToAction("About");
            }
            else
            {
                // Om modellen inte är giltig, visa felmeddelanden
                ViewData["Message"] = "Vänligen fyll i alla obligatoriska fält!";
                return View("About", model);
            }
        }
    }
}
