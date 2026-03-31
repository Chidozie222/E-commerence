using System.ComponentModel.DataAnnotations;

namespace E_commerence.DTOs;

public class ProductDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "It has more than 50 characters")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int StockQuantity { get; set; }
    [Required]
    public int CategoryId { get; set; } 
}