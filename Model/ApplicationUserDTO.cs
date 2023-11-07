using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ApplicationUserDTO
    {
        [Required(ErrorMessage = "Custom error message for First name is required")]
        [MinLength(3, ErrorMessage = "Custom error message for First name length")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Custom error message for Last name is required")]
        [MinLength(3, ErrorMessage = "Custom error message for Last name length")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Custom error message for Email is required")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Custom error message for Email length")]
        [EmailAddress(ErrorMessage = "Custom error message for Email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Custom error message for Phone number is required")]
        [Phone(ErrorMessage = "Custom error message for Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Custom error message for Address is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Custom error message for Address length")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Custom error message for Date of birth is required")]
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
