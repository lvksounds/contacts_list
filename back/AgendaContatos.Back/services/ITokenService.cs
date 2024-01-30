using AgendaContatos.Back.Models;

namespace AgendaContatos.Back.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}