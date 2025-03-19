using System.Text;
using KafkaFlow;
using Newtonsoft.Json;
using Ordering.API.Events;
using Ordering.API.Extensions;
using Ordering.API.Services;

namespace Ordering.API.EventConsumer;

public class BasketCheckoutMiddleware: IMessageMiddleware
{
    private readonly IOrderService _orderService;
    private readonly ILogger<BasketCheckoutMiddleware> _logger;
    
    public BasketCheckoutMiddleware(IOrderService orderService, ILogger<BasketCheckoutMiddleware> logger)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        try
        {
            var jsonString = Encoding.UTF8.GetString(context.Message.Value as byte[]);
            var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(jsonString);

            if (basketCheckoutEvent is null)
            {
                _logger.LogWarning("Invalid message received");
                return;
            }
            //Map BasketCheckoutEvent to Order
            var order = basketCheckoutEvent.ToOrder();
            //Checkout Order
            int res = await _orderService.CheckoutOrder(order);
        
            _logger.LogInformation($"Order {order.Id} processed");
        
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing the message");
            throw;
        }
    }
    
}