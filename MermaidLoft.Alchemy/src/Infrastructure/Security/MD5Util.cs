using System.Security.Cryptography;
using System.Text;
using System;

namespace Infrastructure.Security
{
    public class MD5Util
    {
        private MD5Util() { }

        public static void QucikStart()
        {
            var plaintext = "MermaidLoft";
            var ciphertext = Encode(plaintext);
            Console.WriteLine("plainttext:{0},ciphertext:{1}",plaintext,ciphertext);
        }

        public static string Encode(string plaintext)
        {
            byte[] bytes;
            using (var md5 = MD5.Create())
            {
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
            }
            var result = new StringBuilder();
            foreach (byte t in bytes)
            {
                result.Append(t.ToString("X2"));
            }
            return result.ToString();
        }
    }
}
