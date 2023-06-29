using Diplomski.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diplomski.Pages.UserAuthentication
{
    public class LoginModel : PageModel
    {
        private readonly IUserAuthenticationService _service;

        public LoginModel(IUserAuthenticationService service)
        {
            _service = service;
        }

        [BindProperty]
        public Models.DTO.LoginModel Input { get; set; }
 

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _service.LoginAsync(Input);
            if(result.StatusCode == 1)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["msg"] = result.Message;
                return Page();
            }
        }
    }
}
