using System.ComponentModel.DataAnnotations;

namespace ContactsList.Models
{
    public class User
    {
        public User()
        {

        }

        public User(Guid userId, string name, string email, string password)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Password = password;
        }

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
