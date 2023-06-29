using Diplomski.Data;
using Diplomski.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Diplmoski.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel
            (
            ILogger<IndexModel> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            _context = context;
        }

        public IList<Food> FoodList { get; set; } = default!;
        
        public async Task OnGetAsync(string searchString)
        {
            if (_context.Food != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    List<Food> lista = await _context.Food.Where(s =>
                    s.RestorauntLocation.Contains(searchString)).ToListAsync();

                    FoodList = lista.GroupBy(x => x.Restaurant).Select(y => y.First()).ToList();
                }
                else
                {
                    List<Food> lista = await _context.Food.ToListAsync();

                    FoodList = lista.GroupBy(x => x.Restaurant).Select(y => y.First()).ToList();

                }
            }
        }



    }
}