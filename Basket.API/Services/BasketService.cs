using Basket.API.Entities;
using Basket.API.Repositories;

namespace Basket.API.Services;

public class BasketService: IBasketService
{
    private readonly IBasketRepository _basketRepository;
    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
    }
    public Task<ShoppingCart?> GetBasket(string? userName)
    {
        return _basketRepository.GetBasket(userName);
    }

    public Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        return _basketRepository.UpdateBasket(basket);
    }

    public Task DeleteBasket(string? userName)
    {
        return _basketRepository.DeleteBasket(userName);
    }
}