using System.ComponentModel.DataAnnotations;

namespace E_commerence.DTOs;

public class RegisterDto
{
    [Required]
    [MaxLength(30, ErrorMessage = "The Name is too Long")]
    public string FullName { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "The UserName is too Long")]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(30, ErrorMessage = "The Password is too Long")]
    public string Password { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; } = new DateOnly();
}