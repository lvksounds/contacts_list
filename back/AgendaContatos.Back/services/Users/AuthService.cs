using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.services;
using AgendaContatos.Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
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

                if (activeUser == null) return "InvalidLogin";                

                string token;

                if (!hash.VerifyPassword(user.Password, activeUser.Password))
                {
                    return "InvalidLogin"; 
                }

                token = _tokenService.GenerateToken(activeUser);
                return JsonSerializer.Serialize(new
                {
                    user = new { userName = activeUser.UserName, email = activeUser.Email, id = activeUser.UserId },
                    token,
                });

            }

            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
