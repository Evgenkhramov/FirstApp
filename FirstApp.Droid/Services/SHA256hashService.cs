using FirstApp.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FirstApp.Droid.Services
{
    public class SHA256hashService : ISHA256hashService
    {
        public byte[] GetSHAFromString(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                return bytes;
            }
        }
    }
}