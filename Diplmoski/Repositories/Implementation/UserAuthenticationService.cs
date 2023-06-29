using Diplomski.Data;
using Diplomski.Models.DTO;
using Diplomski.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Security.Claims;

namespace Diplomski.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService
            (
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task<Status> LoginAsync(LoginModel model)
        {
            Status status = new Status();
            var user = await userManager.FindByNameAsync(model.Username);

            // za user
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Neispravno korisničko ime.";
                return status;
            }

            // za lozinku
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Neispravna lozinka.";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                status.StatusCode = 1;
                status.Message = "Uspešna prijava.";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "Korisnik zaključan.";
                return status;
            }
            else
            {
                status.StatusCode = 1;
                status.Message = "Greška pri prijavljivanju.";
                return status;
            }
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Status> RegisterAsync(RegistrationModel model)
        {
            Status status = new Status();
            var userExist = await userManager.FindByNameAsync(model.Username);
            if (userExist != null)
            {
                status.StatusCode = 0;
                status.Message = "Takav korisnik već postoji.";
                return status;
            }

            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Neuspenšo kreiranje.";
                return status;
            }

            // role managment
            if(!await roleManager.RoleExistsAsync(model.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            if(await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "Uspešno ste se registrovali.";

            await SendEmail(model.Email);

            return status;
        }

        // ddaprcgcunxokedz
        public async Task SendEmail(string emailFor)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("peraperictest12345@gmail.com", "FoodExpress - Food delivery");
            mailMessage.To.Add(emailFor);

            mailMessage.Subject = "REGISTRACIJA - FoodExpress";
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Uspešno ste se registrovali na FoodExpress. <br/> Sa ovom porukom ostvarujete" +
                " popust od 20% sa sledeću dostavu hrane na teritoriji Beograda. <br/>" +
                "Srdačan pozdrav,<br/> <b>FoodExpress</b>.";
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("peraperictest12345@gmail.com", "ivscqlyowzjdmqwh");
                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            mailMessage.Dispose();
        }

        
    }
}
