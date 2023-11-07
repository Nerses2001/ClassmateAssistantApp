using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Custom error message for Email is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Custom error message for Email length")]
        [EmailAddress(ErrorMessage = "Custom error message for Email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Custom error message for Password is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Custom error message for Password length")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Custom error message for ConfirmPassword is required")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Custom error message for ConfirmPassword length")]
        [Compare("Password", ErrorMessage = "Custom error message for ConfirmPassword mismatch")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Custom error message for FirstName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Custom error message for FirstName length")]
        [RegularExpression("^(?=.{1,50}$)[A-Z][a-z]+(?:[- ][A-Z][a-z]+)*", ErrorMessage = "Custom error message for FirstName format")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Custom error message for LastName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Custom error message for LastName length")]
        [RegularExpression("^(?=.{1,50}$)[A-Z][a-z]+(?:[- ][A-Z][a-z]+)*", ErrorMessage = "Custom error message for LastName format")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Custom error message for Phone number is required")]
        [Phone(ErrorMessage = "Custom error message for Phone number format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Custom error message for Address is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Custom error message for Address length")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Custom error message for Date of birth is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public RegisterModel(string email, string password, string confirmPassword, string firstName, string lastName, string phoneNumber, string address, DateTime dateOfBirth)
        {
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dateOfBirth;
        }
    }
}
