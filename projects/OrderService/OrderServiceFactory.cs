using Messages;
using OrderService.Core.Repositories;
using OrderService.Core.Helpers;
using OrderService.Core.Mappers;

namespace OrderService;
using MessageClient.Factory;

public static class OrderServiceFactory
{
  public static OrderService CreateOrderService()
  {
    var easyNetQFactory = new EasyNetQFactory();
    var newOrderClient = easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("OrderService", "newOrder");
    var orderCompletionClient = easyNetQFactory.CreatePubSubMessageClient<OrderResponseMessage>("");
    
    var dataContext = new DataContext();
    var orderRepository = new OrderRepository(dataContext);
    var orderRequestMapper = new OrderRequestMapper();
    var orderService = new Core.Services.OrderService(orderRepository,orderRequestMapper);
    var orderResponseMapper = new OrderResponseMapper();
    
    return new OrderService(
      newOrderClient,
      orderCompletionClient,
      orderService,
      orderResponseMapper
    );
  }
}