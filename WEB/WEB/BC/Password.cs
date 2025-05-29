using System.Text;
using System.Security.Cryptography;

namespace BC
{
    public static class Password
    {
        public static Byte[] GenerarHash(string password)
        {
            // Create a sha256
            using (SHA256 hashSha256 = SHA256.Create())
            {
                // this returns a byte array. Compute Hash
                byte[] bytes = hashSha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                return bytes;
            }
        }

        public static string ObtenerHash(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString().ToUpper();
        }
    }
}