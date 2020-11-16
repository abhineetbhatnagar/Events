namespace Events.Tenancy.Services.Infra.JWT.Config  
{
    public class JwtConfig : IJwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryInMinutes { get; set; }
    }

}