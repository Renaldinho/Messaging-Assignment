namespace Messages;

public class OrderRequestMessage
{
    public string CustomerId { get; set; }
    public OrderItemDto[] OrderItems { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}