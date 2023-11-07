using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ApplicationUserDTO
    {
        [Required(ErrorMessage = "Custom error message for FirstName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Custom error message for FirstName length")]
        [RegularExpression("^(?=.{1,50}$)[A-Z][a-z]+(?:[- ][A-Z][a-z]+)*", ErrorMessage = "Custom error message for FirstName format")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Custom error message for LastName is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Custom error message for LastName length")]
        [RegularExpression("^(?=.{1,50}$)[A-Z][a-z]+(?:[- ][A-Z][a-z]+)*", ErrorMessage = "Custom error message for LastName format")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Custom error message for Email is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Custom error message for Email length")]
        [EmailAddress(ErrorMessage = "Custom error message for Email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Address must be between 3 and 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public ApplicationUserDTO(string firstName, string lastName, string email, string phoneNumber, string address, DateTime dateOfBirth)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.DateOfBirth = dateOfBirth;
        }
    }
}
