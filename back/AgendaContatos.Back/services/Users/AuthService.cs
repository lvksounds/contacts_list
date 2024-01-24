using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace AgendaContatos.Back.services.Users
{
    public class AuthService : IAuthService
    {
        private readonly DataBaseContext _dbcontext;

        public AuthService(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> AuthUser(User user)
        {
            try
            {
                User activeUser = _dbcontext.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

                if (activeUser.Equals(null))
                {
                    return "userNotFound";
                }


                // agora é necessário checar a senha do usuário. Porém a criação de senha encriptada ainda necessita ser criada;

                string serilizedUser = JsonSerializer.Serialize(activeUser);

                return serilizedUser;
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
