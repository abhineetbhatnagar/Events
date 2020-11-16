namespace Events.Tenancy.Services.Infra.JWT.Config  
{
    public interface IJwtConfig
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audiance { get; set; }
        int ExpiryInMinutes { get; set; }
    }

}