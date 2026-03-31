using System.ComponentModel.DataAnnotations;

namespace E_commerence.Models;

public class CartItem
{
    [Required]
    public int Id { get; set; }
    public int Quantity { get; set; }
    [Required]
    public string UserId { get; set; }
    public User? User { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}