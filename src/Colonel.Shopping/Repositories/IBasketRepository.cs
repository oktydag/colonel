using Colonel.Shopping.Entities;

namespace Colonel.Shopping.Repositories
{
    public interface IBasketRepository
    {
        Basket GetUserBasket(int userId);
        Basket SaveBasket(Basket basket);
    }
}
