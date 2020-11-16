using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Events.Manager.Services.Infra.Encryption.Config;

namespace Events.Manager.Services.Infra.Encryption.Service{
    public class EncryptionService: IEncryptionService{
        private readonly string _encryptionKey;
        private readonly string _encryptionIV;
        public EncryptionService(IEncryptionConfig encryptionConfig){
            this._encryptionKey = encryptionConfig.Key;
            this._encryptionIV = encryptionConfig.IV;
        }  

        public string Encrypt(string clearText)
        {
            var rj = new RijndaelManaged()
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 128
            };
            byte[] key = Encoding.ASCII.GetBytes(_encryptionKey);
            byte[] iv = Encoding.ASCII.GetBytes(_encryptionIV);

            var encryptor = rj.CreateEncryptor(key, iv);

            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            var toEncrypt = Encoding.ASCII.GetBytes(clearText);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            var encrypted = msEncrypt.ToArray();

            return (Convert.ToBase64String(encrypted));
        }
        

        public string Decrypt(string cipherTextstring)
        {
            string plaintext = string.Empty;
            byte[] cipherText = Convert.FromBase64String(cipherTextstring);
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                byte[] encKey = Encoding.ASCII.GetBytes(_encryptionKey);
                byte[] encIV = Encoding.ASCII.GetBytes(_encryptionIV);

                using AesManaged aes = new AesManaged();
                ICryptoTransform decryptor = aes.CreateDecryptor(encKey, encIV);
                using MemoryStream ms = new MemoryStream(cipherText);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader reader = new StreamReader(cs);
                plaintext = reader.ReadToEnd();
            }
            return plaintext;
        }
                                         
    }
}