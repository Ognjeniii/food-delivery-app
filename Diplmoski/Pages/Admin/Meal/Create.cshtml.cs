using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Diplomski.Data;
using Diplomski.Models;
using Microsoft.AspNetCore.Authorization;

namespace Diplomski.Pages.Admin.Meal
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;

        public List<SelectListItem> GetFoodCategories { get; set; }
        public CreateModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            GetFoodCategories = _context.FoodCategories.Select(i => new SelectListItem
            {
                Text = i.FoodCategoryName,
                Value = i.FoodCategoryName
            }).ToList();
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Food == null || Food == null)
            {
                return Page();
            }

            _context.Food.Add(Food);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
