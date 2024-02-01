using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.services;
using AgendaContatos.Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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
            var hash = new Hash(SHA512.Create());
            try
            {
                User activeUser = await _dbcontext.Users.Where(u => u.UserName == user.UserName).FirstOrDefaultAsync();

                if (activeUser == null) return "userNotFound";                

                string token;

                if (hash.VerifyPassword(user.Password, activeUser.Password))
                {
                    token = _tokenService.GenerateToken(activeUser);
                    return JsonSerializer.Serialize(new
                    {
                        user = new { userName = activeUser.UserName, email = activeUser.Email },
                        token,
                    });
                }
                else
                {
                    return "InvalidUser";
                }
               
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
