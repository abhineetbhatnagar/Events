namespace Events.Tenancy.Services.Infra.Encryption.Config  
{
    public class EncryptionConfig : IEncryptionConfig
    {
        public string Key { get; set; }
        public string IV { get; set; }
    }

}