using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Core.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [Required]
    public string CustomerId { get; set; }

    // Assuming you use lazy loading proxies or ensure the collection is loaded as needed
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
}

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderItemId { get; set; }

    public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Foreign key to Order
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}