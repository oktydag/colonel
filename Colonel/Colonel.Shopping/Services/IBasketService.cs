using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
using System.Collections.Generic;

namespace Colonel.Shopping.Services
{
    public interface IBasketService
    {
        Basket GetUserBasket(int userId);
        Basket SaveBasket(Basket basket);
        bool AddToBasket(AddProductToBasketRequestModel basketItems);
    }
}
