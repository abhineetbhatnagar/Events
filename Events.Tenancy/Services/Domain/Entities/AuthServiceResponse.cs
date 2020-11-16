namespace Events.Tenancy.Services.Domain.Entities
{
    public class AuthServiceResponse{

        // To check weather or not authentication is successful
        public bool Status { get; set; }

        // Authentication token if auth is successful
        public string Auth_Token { get; set; }
    }
}