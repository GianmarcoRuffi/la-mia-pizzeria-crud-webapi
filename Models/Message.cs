using la_mia_pizzeria_static.ValidationCustomClasses;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [StringLength(300, ErrorMessage = "La lunghezza eccede i caratteri consentiti")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [StringLength(300, ErrorMessage = "La lunghezza eccede i caratteri consentiti")]    
    
        public string? Name { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [StringLength(300, ErrorMessage = "La lunghezza eccede i caratteri consentiti")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Campo richiesto")]
        [StringLength(300, ErrorMessage = "La lunghezza eccede i caratteri consentiti")]
        public string? Text { get; set; }


        public Message(string email, string name, string title, string text)
        {

            this.Email = email;
            this.Name = name;
            this.Title = title;
            this.Text = text;
        }
    }







}
