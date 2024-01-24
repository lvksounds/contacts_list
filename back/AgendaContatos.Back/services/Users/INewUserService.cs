using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.services.Users
{
    public interface INewUserService
    {
        Task<string> CreateNewUser(User user);
    }
}