using System;
using System.IO;
namespace AgendaContatos.Back.helpers
{
    public class Base64Converter
    {
        public static byte[] ConvertFromBase64(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        public static void ConvertFromBase64StringToFile(string base64String, string filePath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
