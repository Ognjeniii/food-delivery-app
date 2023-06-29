using Diplomski.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;

namespace Diplomski.Pages.UserAuthentication
{
    public class RegistrationModel : PageModel
    {
        private readonly IUserAuthenticationService _service;

        public RegistrationModel(IUserAuthenticationService service)
            {
            _service = service;
        }

        [BindProperty]
        public Models.DTO.RegistrationModel Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Input.Role = "user";
            var result = await _service.RegisterAsync(Input);

            TempData["msg"] = result.Message;
            return RedirectToPage("/UserAuthentication/Login");
        }

        
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    var model = new Models.DTO.RegistrationModel
        //    {
        //        Username = "admin",
        //        Name = "admin",
        //        Email = "admin@gmail.com",
        //        Password = "Admin123!"
        //    };
        //    model.Role = "admin";
        //    var result = await _service.RegisterAsync(model);
        //    return RedirectToPage("/UserAuthentication/Registration");
        //}
    }
}
