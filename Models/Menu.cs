using System;
namespace Mvc.Models
{
    public class Menu {
        // Properties
        public int id { get; set; }

        public string name { get; set; }

        public string? ingredients { get; set; }

        public int? price { get; set; }
    }
}