using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplomski.Pages.User
{
    public class CheckOutModel : PageModel
    {
        public double bill = Models.Helper.counter;
        public List<string> orders = Models.Helper.ordersList;
        public List<string> foodType = Models.Helper.foodType;
        public List<string> restaurant = Models.Helper.restaurant;
        public void OnGet()
        {
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            Models.Helper.ClearOrder();

            return RedirectToPage("/Index");
        }
    }
}
