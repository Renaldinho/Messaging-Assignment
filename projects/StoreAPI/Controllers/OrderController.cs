using MessageClient;
using Messages;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

internal static class MessageWaiter
{
    public static async Task<T?> WaitForMessage<T>(MessageClient<T> messageClient, string clientId, int timeout = 10000) where T : class
    {
        var tcs = new TaskCompletionSource<T?>();
        var cancellationTokenSource = new CancellationTokenSource(timeout);
        cancellationTokenSource.Token.Register(() => tcs.TrySetResult(default));

        using (var connection = messageClient.ListenUsingTopic<T>(message =>
               {
                   tcs.TrySetResult(message);
               }, "customer" + clientId, clientId))
        {
        }

        return await tcs.Task;
    }
}

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly MessageClient<OrderResponseMessage> _orderResponseMessageClient;
    private readonly MessageClient<OrderRequestMessage> _orderRequestMessageClient;
    
    public OrderController(MessageClient<OrderResponseMessage> orderResponseMessageClient, MessageClient<OrderRequestMessage> orderRequestMessageClient)
    {
        _orderResponseMessageClient = orderResponseMessageClient;
        _orderRequestMessageClient = orderRequestMessageClient;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> PostOrder(OrderRequestMessage orderRequest)
    {
        // Sends new order request message using 'newOrder' topic
        _orderRequestMessageClient.SendUsingTopic(new OrderRequestMessage
        {
            CustomerId = orderRequest.CustomerId,
        }, "newOrder");
        
        // Waits for 'OrderResponseMessage' using 'customerId' topic
        var response = await MessageWaiter.WaitForMessage(_orderResponseMessageClient, orderRequest.CustomerId)!;
        
        if (response != null)
        {
            return Ok(response);
        }
        else
        {
            return StatusCode(408, new OrderResponse { Status = "Order timed out." });
        }
    }
}