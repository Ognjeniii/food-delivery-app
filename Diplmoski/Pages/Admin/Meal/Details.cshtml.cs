﻿using System;
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

namespace Diplomski.Pages.Admin.Meal
{
    [Authorize(Roles = "admin")]

    public class DetailsModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;

        public DetailsModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Food Food { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            else 
            {
                Food = food;
            }
            return Page();
        }
    }
}
