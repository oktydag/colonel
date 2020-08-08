﻿using Colonel.Shopping.Entities;
using System.Collections.Generic;

namespace Colonel.Shopping.Services
{
    public interface IBasketService
    {
        bool AddItemsToBasket(BasketLine basketLine, Basket basket);

        Basket GetUserBasket(int userId);
        BasketLine AddBasketLine(BasketLine basketLine);

        Basket AddBasket(Basket basket);
        bool IncreaseQuantityOfProductInBasket(int userId, int newQuantity);
    }
}
