using System.ComponentModel.DataAnnotations;

namespace Diplomski.Models
{
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naziv kategorije")]
        [Required(ErrorMessage = "Morate da unesete naziv kategorije jela. Ovo polje je obavezno!")]
        public string FoodCategoryName { get; set; }
    }
}
