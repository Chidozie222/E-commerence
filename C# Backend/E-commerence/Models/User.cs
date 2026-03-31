using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace E_commerence.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(30, ErrorMessage = "The Name is too Long")]
        public string FullName { get; set; } = String.Empty;

        public DateOnly DateOfBirth { get; set; } = new DateOnly();
        public bool IsVerified { get; set; } = false;
    }
}