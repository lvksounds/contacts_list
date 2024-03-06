using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Users
{
    public interface IUserService
    {
        Task<string> CreateNewUser(User user);
    }
}