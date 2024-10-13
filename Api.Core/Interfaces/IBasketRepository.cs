using Api.Core.Entites;

namespace Api.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id); //get Basket

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket); //updateBasket

        Task<bool> DeleteBasket(string id);
    }
}