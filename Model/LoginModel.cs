using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(120, ErrorMessage = "Email should not exceed 120 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password should be between 8 and 50 characters")]
        public string Password { get; set; }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
