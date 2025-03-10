using Discount.API.Entities;
using Discount.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    
    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
    }
    
    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var coupon = await _discountService.GetDiscount(productName);
        return Ok(coupon);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await _discountService.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await _discountService.UpdateDiscount(coupon));
    }

    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteDiscount(string productName)
    {
        await _discountService.DeleteDiscount(productName);
        return Ok();
    }
    
}