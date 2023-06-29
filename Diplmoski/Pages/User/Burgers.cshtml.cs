using Diplomski.Data;
using Diplomski.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Diplomski.Pages.User
{
    [Authorize]
    public class BurgersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BurgersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Food Food { get; set; } = default!;

        public List<Food> FoodList { get; set; } = default!;

        public async Task OnGet()
        {
            FoodList = await _context.Food.Where(s => s.FoodType == "Burger").ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(Food? food)
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

            FoodList = await _context.Food.Where(s => s.FoodType == "Burger").ToListAsync();
            return Page();
        }
    }
}
