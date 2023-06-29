using Microsoft.AspNetCore.Identity;

namespace Diplomski.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
