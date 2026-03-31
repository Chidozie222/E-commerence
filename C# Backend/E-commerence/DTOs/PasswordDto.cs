using System.ComponentModel.DataAnnotations;

namespace E_commerence.DTOs;

public class PasswordDto
{
    [Required]
    [MaxLength(30, ErrorMessage = "The Password is too Long")]
    public string password { get; set; }
}