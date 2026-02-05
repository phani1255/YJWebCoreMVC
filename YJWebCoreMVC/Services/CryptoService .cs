/*
 Created by Phanindra 05-Feb-2026
 */
using System.Security.Cryptography;

namespace YJWebCoreMVC.Services
{
    public interface ICryptoService
    {
        byte[] Encrypt(byte[] data, byte[] key, byte[] iv);
        byte[] Decrypt(byte[] data, byte[] key, byte[] iv);
    }

    public class CryptoService : ICryptoService
    {
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(data, 0, data.Length);
        }
    }
}
