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

        public string GetNewUserId()
        {
            string id = (_dataBaseContext.Users.Count() + 1).ToString();

            return id;
        }

        public async Task<string> CreateNewUser(User user)
        {
            User NewUser = user;

            NewUser.UserId = GetNewUserId();

            if (NewUser == null)
            {
                return "UserNull";
            }

            var alreadyHasUser = await _dataBaseContext.Users.Where(u => u.UserId == NewUser.UserId).FirstAsync();

            if (!alreadyHasUser.Equals(null))
            {
                _dataBaseContext.Add<User>(NewUser);
                return "UserCreated";
            }

            return "Error";
        }
    }
}
