using Colonel.Shopping.Models.Price;

namespace Colonel.Shopping.Services
{
    public interface IPriceService
    {
        PriceResponseModel GetProductPrice(PriceRequestModel priceRequestModel);
    }
}
