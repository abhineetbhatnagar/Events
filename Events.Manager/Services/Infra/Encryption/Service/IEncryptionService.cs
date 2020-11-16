namespace Events.Manager.Services.Infra.Encryption.Service{
    public interface IEncryptionService{
        string Encrypt(string clearText);
        string Decrypt(string cipherTextstring);
    }
}