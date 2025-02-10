using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-postadress är obligatorisk.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Meddelande är obligatoriskt.")]
        public string Message { get; set; }
    }
}
