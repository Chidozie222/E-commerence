using System.ComponentModel.DataAnnotations;

namespace E_commerence.Models;

public class Categroy
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<Product> Products { get; set; }
}