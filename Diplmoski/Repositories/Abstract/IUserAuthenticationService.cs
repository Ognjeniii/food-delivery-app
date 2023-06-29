using Diplomski.Models.DTO;

namespace Diplomski.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        Task SendEmail(string emailFor);
    }
}
