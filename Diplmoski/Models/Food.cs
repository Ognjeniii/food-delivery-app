using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Diplomski.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naziv jela")]
        [Required(ErrorMessage = "Morate da uneste naziv jela. Ovo polje je obavezno!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Naziv jela je previse kratak. Unesite validan pojam.")]
        public string FoodName { get; set; }

        [Display(Name = "Kategorija jela")]
        [Required(ErrorMessage = "Morate da unesete kategoriju proizvoda! Ovo polje je obavezno")]
        public string FoodType { get; set; }

        [Display(Name = "Količina/Veličina")]
        [Required(ErrorMessage = "Morate da unesete količinu/veličinu proizvoda! Ovo polje je obavezno")]
        public string Quantity { get; set; }

        [Display(Name = "Cena")]
        [RegularExpression("\\d+", ErrorMessage = "Ovo polje prihvata samo bojeve.")]
        [Required(ErrorMessage = "Morate da unesete cenu proizvoda! Ovo polje je obavezno")]
        public double Price { get; set; }

        [Display(Name = "Kratak opis")]
        [Required(ErrorMessage = "Morate da unesete kratak opis! Ovo polje je obavezno")]
        public string Description { get; set; }

        [Display(Name = "Restoran")]
        [Required(ErrorMessage = "Morate da unesete restoran! Ovo polje je obavezno")]
        public string Restaurant { get; set; }

        [Display(Name = "Lokacija restorana")]
        [Required(ErrorMessage = "Morate da unesete lokaciju restorana! Ovo polje je obavezno")]
        public string RestorauntLocation { get; set; }

    }
}
