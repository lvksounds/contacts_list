using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.services;
using AgendaContatos.Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace AgendaContatos.Back.services.Users
{
    public class AuthService : IAuthService
    {
        private readonly DataBaseContext _dbcontext;
        private readonly ITokenService _tokenService;

        public AuthService(DataBaseContext dbcontext, ITokenService tokenService)
        {
            _dbcontext = dbcontext;
            _tokenService = tokenService;
        }

        public async Task<string> AuthUser(User user)
        {
            try
            {
                User activeUser = await _dbcontext.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();

                if (activeUser == null) return "userNotFound";                
                // agora é necessário checar a senha do usuário. Porém a criação de senha encriptada ainda necessita ser criada;

                // gera token 

                var token = _tokenService.GenerateToken(activeUser);
                

                return JsonSerializer.Serialize(new
                {
                    user = activeUser,
                    token = token,
                });
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
