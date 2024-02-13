using Mapper;
using Messages;
using OrderService.Core.Entities;

namespace OrderService.Core.Mappers;

// TODO: Modify or create mappers as you see fit
public class OrderRequestMapper : IMapper<OrderRequestMessage, Order>
{
    public OrderRequestMessage Map(Order model)
    {
        return new OrderRequestMessage
        {
        };
    }

    public Order Map(OrderRequestMessage model)
    {
        var order = new Order
        {
            CustomerId = model.CustomerId,
            OrderItems = model.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity
            }).ToList()
        };

        return order;
    }
}

public class OrderResponseMapper : IMapper<OrderResponseMessage, Order>
{
    public OrderResponseMessage Map(Order model)
    {
        return new OrderResponseMessage
        {
        };
    }

    public Order Map(OrderResponseMessage model)
    {
        return new Order
        {
        };
    }
}