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

[HttpPost]
[Route("/omoss")]
public IActionResult About(ContactFormModel model)
{
    if (ModelState.IsValid)
    {
        // Filens sökväg
        string filePath = "contactMessages.json";

        // Läs in befintliga meddelanden
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
        ViewData["Message"] = "Ditt meddelande har sparats!";
        return View(new ContactFormModel());  // Återställ formuläret
    }

    return View(model);
}


    }
}
