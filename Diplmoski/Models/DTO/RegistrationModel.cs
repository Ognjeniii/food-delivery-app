using System.ComponentModel.DataAnnotations;

namespace Diplomski.Models.DTO
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Ime je obavezno polje pri registraciji.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email je obavezno polje pri registraciji.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno polje pri registraciji.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje pri registraciji.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#$^+=!*()@%&]).{6,}$",
            ErrorMessage = "Minimalna duzina je 6 karaktera, i mora da sadrzi bar jedno veliko slovo," +
            " jedno malo slovo, jedan specijalan karakter i jedan broj.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ponovljena lozinka je obavezno polje pri registraciji.")]
        [Compare("Password")] 
        public string PasswordConfirmed { get; set; }

        public string? Role { get; set; }
    }
}
