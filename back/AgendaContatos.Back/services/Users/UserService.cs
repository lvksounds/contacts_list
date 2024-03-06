using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AgendaContatos.Back.services.Users
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext _dataBaseContext;

        public UserService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public async Task<string> CreateNewUser(User user)
        {
            var hash = new Hash(SHA512.Create());
            if (user == null)
            {
                return "UserNull";
            }

            User NewUser = user;
            NewUser.UserId = GetNewUserId();

            var passwordHash = hash.EncriptyPassword(user.Password);
            NewUser.Password = passwordHash;
            
            try
            {
                await _dataBaseContext.Users.AddAsync(NewUser);
                await _dataBaseContext.SaveChangesAsync();
                return "UserCreated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            

            
            
        }

        private string GetNewUserId()
        {
            string id = (_dataBaseContext.Users.Count() + 1).ToString();

            return id;
        }
    }
}
