namespace Events.Manager.Services.Infra.Encryption.Config  
{
    public interface IEncryptionConfig
    {
        string Key { get; set; }
        string IV { get; set; }
    }

}