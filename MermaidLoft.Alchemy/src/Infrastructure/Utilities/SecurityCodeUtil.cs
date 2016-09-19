using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities
{
    public class SecurityCodeUtil
    {
        private SecurityCodeUtil() { }

        public static string Md5(string plaintext)
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
