namespace StoreAPI.Models;

public class OrderRequest
{
    public string CustomerId { get; set; }
    public OrderItemDto[] OrderItems { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderResponse
{
    public string Status { get; set; }
}