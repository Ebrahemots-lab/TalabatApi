using System.Text.Json;
using Api.Core.Entites;
using Api.Core.Interfaces;
using StackExchange.Redis;

namespace Api.Data.Repositories
{
    public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {

            var radisValue = await _database.StringGetAsync(id);
            if (radisValue.HasValue)
            {
                //Deserialize from json to customer basket
                var customerBasket = JsonSerializer.Deserialize<CustomerBasket>(radisValue);
                return customerBasket;
            }
            return null;
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }


        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            //Serialize  customer basket to json
            var basketJson = JsonSerializer.Serialize(basket);

            //update the basket
            //If founded => update
            //Else => create 
            var flag = await _database.StringSetAsync(basket.Id, basketJson, TimeSpan.FromDays(1));

            if (flag)
            {
                return await GetBasketAsync(basket.Id);
            }

            return null;

        }
    }
}