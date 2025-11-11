using System.Security.Cryptography;
using System.Text;

namespace Gestion.Api.Helpers
{
    public class EncryptionHelper
    {
        public static string EncryptAES(string plainText, string secretKey)
        {
            try
            {
                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.GenerateIV(); // IV aleatorio para cada cifrado

                using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var ms = new MemoryStream();

                // 🔒 Ciframos el texto plano
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                var encryptedBytes = ms.ToArray();

                // 🔗 Concatenamos IV + datos cifrados
                var combined = new byte[aes.IV.Length + encryptedBytes.Length];
                Array.Copy(aes.IV, 0, combined, 0, aes.IV.Length);
                Array.Copy(encryptedBytes, 0, combined, aes.IV.Length, encryptedBytes.Length);

                // 🔤 Codificamos todo en Base64 para enviar
                return Convert.ToBase64String(combined);
            }
            catch
            {
                throw new InvalidOperationException("No se pudo encriptar el texto.");
            }
        }


        public static string DecryptAES(string cipherTextBase64, string secretKey)
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
