using Microsoft.Build.Framework;

namespace E_commerence.Models;

public class OrderItem
{
    [Required]
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    [Required]
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}