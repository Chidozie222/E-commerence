using System.ComponentModel.DataAnnotations;

namespace E_commerence.Models;

public class Product
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "It has more than 50 characters")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int CategoryId { get; set; }
    public Categroy? Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    
}