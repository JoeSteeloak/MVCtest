using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using Newtonsoft.Json;
using System.IO;


namespace Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ViewBag och ViewData för vanliga meddelanden
            ViewBag.Greeting = "Välkommen till vår Pizzeria!";
            ViewData["OpeningHours"] = "Vi har öppet alla dagar 11:00 - 23:00";

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
            // Hämta sparad data från session om den finns
            var model = new ContactFormModel(); // Tom modell om inget finns i session
            return View(model); 
        }

        // Metod för att skicka till JSON
        [HttpPost]
[Route("/omoss/skicka")]
public IActionResult SendToJson(ContactFormModel model)
{
    if (ModelState.IsValid)
    {
        // Läs existerande data från JSON-fil
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

        // Visa bekräftelsemeddelande
        ViewData["Message"] = "Ditt meddelande har skickats och sparats!";
        return View("About", model); // Om det var lyckat, återgå till About
    }
    else
    {
        // Om modellen inte är giltig, visa felmeddelanden
        ViewData["Message"] = "Vänligen fyll i alla obligatoriska fält!";
        return View("About", model); // Visa samma vy med felmeddelanden
    }}}}