using System.ComponentModel.DataAnnotations;

namespace Events.Tenancy.Services.Domain{
    public class LoginModel{
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide username.")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide password.")]
        public string Password { get; set; }
    }
}