using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class ApplicationUser : IdentityUser
    {
        private string _address = string.Empty;
        [MinLength(3)]
        public string? FirstName { get; set; }
        [MinLength(3)]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address
        {
            get { return _address; }
            set { _address = value.Replace(".", " ").Replace("  ", " "); }
        }
        public virtual ICollection<UserCourse>? UserCourses { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}";
        public string? AccessToken { get; set; }
    }
}