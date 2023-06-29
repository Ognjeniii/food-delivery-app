using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Diplomski.Data;
using Diplomski.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Diplomski.Pages.Admin.Category
{
    [Authorize(Roles = "admin")]

    public class DeleteModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;

        public DeleteModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public FoodCategory FoodCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FoodCategories == null)
            {
                return NotFound();
            }

            var foodcategory = await _context.FoodCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (foodcategory == null)
            {
                return NotFound();
            }
            else 
            {
                FoodCategory = foodcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FoodCategories == null)
            {
                return NotFound();
            }
            var foodcategory = await _context.FoodCategories.FindAsync(id);

            if (foodcategory != null)
            {
                FoodCategory = foodcategory;
                _context.FoodCategories.Remove(FoodCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
