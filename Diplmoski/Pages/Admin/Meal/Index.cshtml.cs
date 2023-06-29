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

namespace Diplomski.Pages.Admin.Meal
{
    [Authorize(Roles = "admin")]

    public class IndexModel : PageModel
    {
        private readonly Diplomski.Data.ApplicationDbContext _context;
        public string FoodNameSort { get; set; }
        public string FoodTypeSort { get; set; }
        public string QuantitySort { get; set; }
        public string RestaurantSort { get; set; }

        public string CurrentFilter { get; set; }


        public IndexModel(Diplomski.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,string searchString)
        {
            if (_context.Food != null)
            {
                FoodNameSort = sortOrder == "foodname_asc_sort" ? "foodname_desc_sort" : "foodname_asc_sort";
                FoodTypeSort = sortOrder == "foodtype_asc_sort" ? "foodtype_desc_sort" : "foodtype_asc_sort";
                QuantitySort = sortOrder == "quantity_asc_sort" ? "quantity_desc_sort" : "quantity_asc_sort";
                RestaurantSort = sortOrder == "restaurant_asc_sort" ? "restaurant_desc_sort" : "restaurant_asc_sort";

                if(!String.IsNullOrEmpty(searchString))
                {
                    Food = await _context.Food.Where(s => s.FoodName.Contains(searchString)
                    || s.FoodType.Contains(searchString)
                    || s.Quantity.Contains(searchString)
                    || s.Restaurant.Contains(searchString)).ToListAsync();
                }
                else
                {
                    Food = await _context.Food.ToListAsync();
                }

                switch (sortOrder)
                {
                    case "foodname_asc_sort":
                        Food = Food.OrderBy(s => s.FoodName).ToList();
                        break;
                    case "foodname_desc_sort":
                        Food = Food.OrderByDescending(s => s.FoodName).ToList();
                        break;

                    case "foodtype_asc_sort":
                        Food = Food.OrderBy(s => s.FoodType).ToList();
                        break;
                    case "foodtype_desc_sort":
                        Food = Food.OrderByDescending(s => s.FoodType).ToList();
                        break;

                    case "quantity_asc_sort":
                        Food = Food.OrderBy(s => s.Quantity).ToList();
                        break;
                    case "quantity_desc_sort":
                        Food = Food.OrderByDescending(s => s.Quantity).ToList();
                        break;

                    case "restaurant_asc_sort":
                        Food = Food.OrderBy(s => s.Restaurant).ToList();
                        break;
                    case "restaurant_desc_sort":
                        Food = Food.OrderByDescending(s => s.Restaurant).ToList();
                        break;

                    default:
                        Food = Food.OrderBy(s => s.FoodName).ToList();
                        break;
                }
            }
        }
    }
}
