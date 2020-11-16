namespace Events.Tenancy.Services.Infra.JWT.Config  
{
    public class JwtConfig : IJwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public int ExpiryInMinutes { get; set; }
    }

}