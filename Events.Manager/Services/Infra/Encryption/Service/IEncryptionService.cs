namespace Events.Manager.Services.Infra.Encryption.Service{
    public interface IEncryptionService{

        // Method to encrypt a plain string
        string Encrypt(string clearText);

        // Method to decrypt an encrypted string
        string Decrypt(string cipherTextstring);
    }
}