using System;
using System.Security.Cryptography;

namespace RealEstateManager.Utils
{
    public static class PasswordHelper
    {
        private const int HashSize = 32;
        private const int SaltSize = 32;
        private const int Iterations = 1000;

        public static string GetHashedPassword(string plainTextPassword, out string salt)
        {
            using (var algorithm = new Rfc2898DeriveBytes(plainTextPassword, SaltSize, Iterations))
            {
                var hashBytes = algorithm.GetBytes(HashSize);

                salt = Convert.ToBase64String(algorithm.Salt);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool ValidateHash(string plainTextPassword, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var algorithm = new Rfc2898DeriveBytes(plainTextPassword, saltBytes, Iterations))
            {
                var hashBytesNew = algorithm.GetBytes(HashSize);

                return hash == Convert.ToBase64String(hashBytesNew);
            }
        }
    }
}
