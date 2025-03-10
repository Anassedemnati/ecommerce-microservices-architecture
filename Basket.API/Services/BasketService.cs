using Basket.API.Entities;
using Basket.API.Models;
using Basket.API.Repositories;
using Basket.API.RestServices;

namespace Basket.API.Services;

public class BasketService: IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IDiscountRestService _discountRestService;
    public BasketService(IBasketRepository basketRepository, IDiscountRestService discountRestService)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _discountRestService = discountRestService ?? throw new ArgumentNullException(nameof(discountRestService));
    }

    public async Task<ShoppingCart?> GetBasket(string? userName)
    {
        return await _basketRepository.GetBasket(userName);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            CouponModel? coupon = await _discountRestService.GetDiscount(item.ProductName);
            if (coupon is not null)
            {
                item.Price -= coupon.Amount;
            }
        }
        return await _basketRepository.UpdateBasket(basket);
    }

    public async Task DeleteBasket(string? userName)
    {
        await _basketRepository.DeleteBasket(userName);
    }
}