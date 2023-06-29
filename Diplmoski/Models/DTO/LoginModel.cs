using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;



namespace Diplomski.Models.DTO
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Morate da unesete korisničko ime da bi ste se prijavili.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Morate da unesete lozinku da biste se prijavili.")]
        public string Password { get; set; }

    }
}
