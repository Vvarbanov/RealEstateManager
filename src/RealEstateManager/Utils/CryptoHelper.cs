using System;
using System.IO;
using System.Security.Cryptography;

namespace RealEstateManager.Utils
{
    public static class CryptoHelper
    {
        public static string EncryptAgentName(string plainText)
        {
            var userKey = Convert.FromBase64String(ConfigReader.UserKey);
            var userInitializationVector = Convert.FromBase64String(ConfigReader.UserInitializationVector);

            return EncryptStringToBytes(plainText, userKey, userInitializationVector);
        }

        public static string DecryptAgentName(string cryptoText)
        {
            var userKey = Convert.FromBase64String(ConfigReader.UserKey);
            var userInitializationVector = Convert.FromBase64String(ConfigReader.UserInitializationVector);

            return DecryptStringFromBytes(cryptoText, userKey, userInitializationVector);
        }

        private static string EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            byte[] encrypted;

            using (var algorithm = new RijndaelManaged())
            {
                algorithm.Key = key;
                algorithm.IV = iv;

                var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using (var stream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(plainText);
                        }

                        encrypted = stream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        private static string DecryptStringFromBytes(string cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));

            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            string plainText;

            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                var cipherTextByteArray = Convert.FromBase64String(cipherText);

                using (var stream = new MemoryStream(cipherTextByteArray))
                {
                    using (var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            plainText = reader.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }
    }
}
