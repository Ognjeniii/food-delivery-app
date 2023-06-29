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

namespace Diplomski.Pages.Admin.Category
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;

        public CreateModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FoodCategory FoodCategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FoodCategories == null || FoodCategory == null)
            {
                return Page();
            }

            _context.FoodCategories.Add(FoodCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
