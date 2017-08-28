using System;
using System.Text;

namespace Infrastructure.Security
{
    public class Base64Util
    {
        private Base64Util() { }

        public static void QucikStart()
        {
            var plaintext = "[{\"available\":true,\"host\":\"101.200.83.24\",\"port\":8586,\"weight\":1}]";
            var ciphertext = Encode(plaintext);
            var clip = "W3siYXZhaWxhYmxlIjp0cnVlLCJob3N0IjoiMTAxLjIwMC44My4yNCIsInBvcnQiOjg1ODYsIndlaWdodCI6MX1d";
            Decode(clip);
        }
        public static string Encode(string plaintext)
        {
            Console.WriteLine("plaintext:{0}", plaintext);
            string ciphertext = string.Empty;
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            ciphertext = Convert.ToBase64String(bytes);
            Console.WriteLine("ciphertext: " + ciphertext);
            return ciphertext;
        }

        public static string Decode(string ciphertext)
        {
            Console.WriteLine("ciphertext: " + ciphertext);
            string plaintext = string.Empty;
            byte[] bytes = Convert.FromBase64String(ciphertext);
            plaintext = Encoding.UTF8.GetString(bytes);
            Console.WriteLine("plaintext:{0}", plaintext);
            return plaintext;
        }
    }
}
