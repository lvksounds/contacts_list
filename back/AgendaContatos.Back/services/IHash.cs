namespace AgendaContatos.Back.Services
{
    public interface IHash
    {
        string EncriptyPassword(string password);
        bool VerifyPassword(string passedPassword, string savedPassword);
    }
}