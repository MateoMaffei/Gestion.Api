using System.Security.Cryptography;
using System.Text;

namespace Gestion.Api.Helpers
{
    public class EncryptionHelper
    {
        public static string DecryptStringAES(string cipherTextBase64, string secretKey)
        {
            try
            {
                // Texto cifrado (desde el front) llega en Base64
                var fullCipher = Convert.FromBase64String(cipherTextBase64);

                // Tomamos los primeros 16 bytes como IV
                var iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                var cipher = new byte[fullCipher.Length - iv.Length];
                Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream(cipher);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                throw new InvalidOperationException("No se pudo desencriptar el password recibido.");
            }
        }
    }
}
