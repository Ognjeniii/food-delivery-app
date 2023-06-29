using Diplomski.Data;
using Diplomski.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Diplomski.Pages.User
{
    [Authorize]
    public class PancakesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PancakesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Food Food { get; set; } = default!;

        public IList<Food> FoodList { get; set; } = default!;


        public async Task OnGet()
        {
            FoodList = await _context.Food.Where(s => s.FoodType == "Palacinka" || s.FoodType == "Palačinka").ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(Food food)
        {
            if (food.Id == null)
            {
                return NotFound();
            }

            var foodFounded = await _context.Food.FirstOrDefaultAsync(m => m.Id == food.Id);

            if (foodFounded != null)
            {
                Models.Helper.AddParams(foodFounded.Price, foodFounded.FoodName, foodFounded.FoodType, foodFounded.Restaurant);
            }

            FoodList = await _context.Food.Where(s => s.FoodType == "Palacinka" || s.FoodType == "Palačinka").ToListAsync();
            return Page();
        }
    }
}
