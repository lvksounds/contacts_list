using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Users
{
    public interface IAuthService
    {
        Task<string> AuthUser(User user);
    }
}