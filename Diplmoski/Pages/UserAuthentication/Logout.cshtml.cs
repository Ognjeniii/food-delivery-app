using Diplomski.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplomski.Pages.UserAuthentication
{
    public class LogoutModel : PageModel
    {
        private readonly IUserAuthenticationService _service;

        public LogoutModel(IUserAuthenticationService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.LogoutAsync();
            Models.Helper.ClearOrder();
            return RedirectToPage("/UserAuthentication/Login");
        }
    }
}
