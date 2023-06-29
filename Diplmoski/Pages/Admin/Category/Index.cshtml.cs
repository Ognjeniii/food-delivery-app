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

    public class IndexModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;

        public IndexModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FoodCategory> FoodCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodCategories != null)
            {
                FoodCategory = await _context.FoodCategories.ToListAsync();
            }
        }
    }
}
