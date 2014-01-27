using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Services.Helpers
{
    public class SaltedHash
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public SaltedHash(string password)
        {
            var saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
                provider.GetNonZeroBytes(saltBytes);

            Salt = Convert.ToBase64String(saltBytes);
            Hash = ComputeHash(Salt, password);
        }

        static string ComputeHash(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000))
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        }

        public static bool Verify(string salt, string hash, string password)
        {
            return hash == ComputeHash(salt, password);
        }
    }
}
