namespace Ordering.API.Events;

public class BasketCheckoutEventItem
{
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}