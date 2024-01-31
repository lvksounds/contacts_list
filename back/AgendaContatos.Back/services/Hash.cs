using System.Security.Cryptography;
using System.Text;

namespace AgendaContatos.Back.Services
{
    public class Hash : IHash
    {
        private HashAlgorithm _hashAlgorithm;

        public Hash(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public string EncriptyPassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);
            var encriptedPassword = _hashAlgorithm.ComputeHash(encodedPassword);

            var sb = new StringBuilder();
            foreach (var c in encriptedPassword)
            {
                sb.Append(c.ToString("X2"));
            }

            return sb.ToString();
        }

        public bool VerifyPassword(string passedPassword, string savedPassword)
        {
            if (string.IsNullOrEmpty(savedPassword)) throw new NullReferenceException("Cadastre uma senha");

            var encryptedPassword = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(passedPassword));
            var sb = new StringBuilder();
            foreach (var c in encryptedPassword)
            {
                sb.Append(c.ToString("X2"));
            }

            return sb.ToString() == savedPassword;
        }
    }
}
