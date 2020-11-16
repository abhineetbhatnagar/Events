namespace Events.Tenancy.Services.Infra.Encryption.Service{
    public interface IEncryptionService{

        /// <summary>
        /// Method to encrypt a plain string
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        string Encrypt(string clearText);

        /// <summary>
        /// Method to decrypt an encrypted string
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        string Decrypt(string cipherTextstring);
    }
}