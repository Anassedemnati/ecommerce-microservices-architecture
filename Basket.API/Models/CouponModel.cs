namespace Basket.API.Models;

public class CouponModel
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? Description { get; set; }
    public int Amount { get; set; }
}