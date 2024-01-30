using System.Globalization;

namespace AgendaContatos.Back.Models
{
    public class User
    {
        public User() { }
        public User(User user)
        {
            UserName = user.UserName; 
            Password = user.Password;
            Email = user.Email;
        }
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string? Email { get; set; }
    }
}
