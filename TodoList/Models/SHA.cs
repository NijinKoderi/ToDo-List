using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TodoList.Models
{
    public class SHA
    {
        public static string Encode(string value)
        {
            // Test data
            const string PASS = "6D114A94-375E-4FFD-9F48-CF2C7A12620F";
            const string SALT = "E5A8DB1B-5EAB-4C62-B322-CE24CE274303";
            byte[] utfdata = UTF8Encoding.UTF8.GetBytes(value);
            byte[] saltBytes = UTF8Encoding.UTF8.GetBytes(SALT);

            // We're using the PBKDF2 standard for password-based key generation
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(
                PASS, saltBytes, 1000);

            // Our symmetric encryption algorithm
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            // Encryption
            ICryptoTransform encryptTransf = aes.CreateEncryptor();

            // Output stream, can be also a FileStream
            MemoryStream encryptStream = new MemoryStream();
            CryptoStream encryptor = new CryptoStream(
                encryptStream, encryptTransf, CryptoStreamMode.Write);
            encryptor.Write(utfdata, 0, utfdata.Length);
            encryptor.Flush();
            encryptor.Close();

            // Showing our encrypted content
            byte[] encryptBytes = encryptStream.ToArray();
            string encryptedString = Convert.ToBase64String(encryptBytes);

            // Close stream
            encryptStream.Close();

            // Return encrypted text
            return encryptedString;
        }
        public static string Decode(string value)
        {
            const string PASS = "6D114A94-375E-4FFD-9F48-CF2C7A12620F";
            const string SALT = "E5A8DB1B-5EAB-4C62-B322-CE24CE274303";
            // Get inputs as bytes

            byte[] encryptBytes = Convert.FromBase64String(value);
            byte[] saltBytes = Encoding.UTF8.GetBytes(SALT);

            // We're using the PBKDF2 standard for password-based key generation
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(
                PASS, saltBytes);

            // Our symmetric encryption algorithm
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            aes.Key = rfc.GetBytes(aes.KeySize / 8);
            aes.IV = rfc.GetBytes(aes.BlockSize / 8);

            // Now, decryption
            ICryptoTransform decryptTrans = aes.CreateDecryptor();

            // Output stream, can be also a FileStream
            MemoryStream decryptStream = new MemoryStream();
            CryptoStream decryptor = new CryptoStream(
                decryptStream, decryptTrans, CryptoStreamMode.Write);
            decryptor.Write(encryptBytes, 0, encryptBytes.Length);
            decryptor.Flush();
            decryptor.Close();

            // Showing our decrypted content
            byte[] decryptBytes = decryptStream.ToArray();
            string decryptedString = UTF8Encoding.UTF8.GetString(
                decryptBytes, 0, decryptBytes.Length);

            // Close Stream
            decryptStream.Close();

            // Return decrypted text
            return decryptedString;
        }
    }
}