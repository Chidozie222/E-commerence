using System.ComponentModel.DataAnnotations;

namespace E_commerence.DTOs;

public class CategroyDto
{
    [Required]
    [MaxLength(50, ErrorMessage =  "Name must be less than 50 characters")]
    public string Name { get; set; }

    public string Description { get; set; } = string.Empty;
}

public class CategroyUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; } 
}