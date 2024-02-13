using Messages;
using OrderService.Core.Entities;
using OrderService.Core.Mappers;
using Repository;

namespace OrderService.Core.Services;

// TODO: Modify this service as needed
public class OrderService
{
    private readonly IRepository<Order> _repository;
    private readonly OrderRequestMapper _requestMapper;
    
    public OrderService(IRepository<Order> repository, OrderRequestMapper requestMapper)
    {
        _repository = repository;
        _requestMapper = requestMapper;
    }
    
    public Order GetOrder(int id)
    {
        return _repository.GetById(id);
    }
    
    public void CreateOrder(OrderRequestMessage orderRequestMessage)
    {
        // Use the mapper to convert the message to an Order entity
        var order = _requestMapper.Map(orderRequestMessage);
        
        // Add the mapped order to the repository
        _repository.Add(order);
    }

    public void CompleteOrder(Order order)
    {
        _repository.Update(order);
    }

    public IEnumerable<Order> GetAllOrders()
    {
        return _repository.GetAll();
    }
}