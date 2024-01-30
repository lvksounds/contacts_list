using AgendaContatos.Back.data;
using AgendaContatos.Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (user == null)
            {
                return "UserNull";
            }

            User NewUser = user;
            NewUser.UserId = GetNewUserId();
            
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
