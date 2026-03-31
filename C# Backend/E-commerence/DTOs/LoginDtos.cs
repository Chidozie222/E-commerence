using System.ComponentModel.DataAnnotations;

namespace E_commerence.DTOs;

public class LoginDtos
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(30, ErrorMessage = "The Password is too Long")]
    public string Password { get; set; }
}