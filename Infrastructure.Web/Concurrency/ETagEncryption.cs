using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Web.Concurrency
{
    public static class ETagEncryption
    {
        // Disclaimer: this is not recommended secure encryption. Just for demo purposes
        private static byte[] RGB = new byte[] { 
            134, 209, 1, 34, 108, 89, 23, 42 ,            
            134, 209, 1, 34, 108, 19, 23, 42
        };

        /// <summary>
        /// Decrypts a encrypted Base64 string.
        /// </summary>
        public static string Decrypt(string base64, string iv)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                // TODO: change to use hash of IV
                using (var transform = provider.CreateDecryptor(RGB, Encoding.ASCII.GetBytes(iv.PadRight(16).Substring(0, 16))))
                {
                    var buffer = Convert.FromBase64String(base64);
                    var finalBlock = transform.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(finalBlock).Trim();
                }
            }
        }

        /// <summary>
        /// Encrypts string data into a encrypted Base64 string.
        /// </summary>
        public static string Encrypt(string data, string iv)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                // TODO: change to use hash of IV
                using (var transform = provider.CreateEncryptor(RGB, Encoding.ASCII.GetBytes(iv.PadRight(16).Substring(0, 16))))
                {
                    var buffer = Encoding.UTF8.GetBytes(data.PadRight(16));
                    var finalBlock = transform.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Convert.ToBase64String(finalBlock);
                }
            }

        }
    }
}
