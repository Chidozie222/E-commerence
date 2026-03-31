using E_commerence.Enums;
using Microsoft.Build.Framework;

namespace E_commerence.Models;

public class Order
{
    [Required]
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string ShippingAddress { get; set; }
    [Required]
    public string UserId { get; set; }
    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}