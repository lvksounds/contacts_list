using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using AgendaContatos.Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AgendaContatos.Back.services.Users
{
    public class NewUserService : INewUserService
    {
        private readonly DataBaseContext _dataBaseContext;

        public NewUserService(DataBaseContext dataBaseContext)
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
            
            _dataBaseContext.Users.Add(NewUser);
            _dataBaseContext.SaveChanges();

            return "UserCreated";
            
            
        }

        private string GetNewUserId()
        {
            string id = (_dataBaseContext.Users.Count() + 1).ToString();

            return id;
        }
    }
}
