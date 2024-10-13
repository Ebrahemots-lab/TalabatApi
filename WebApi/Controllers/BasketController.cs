using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Entites;
using Api.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BasketController(IBasketRepository repo) : BaseApiController
    {
        private readonly IBasketRepository _repo = repo;
        //Impelement the methods in IBasketRepo 

        [HttpGet]
        public async Task<CustomerBasket> GetCustomerBasket(string id)
        {
            //Two cases 
            //1 - Nullable CustomerBasket => Create new CustomerBasket and return it because the client already has basket but it's expired
            //2 - Non - Nullable CustomerBasket => return customerbasket
            var customerBasket = await _repo.GetBasketAsync(id);
            return customerBasket is null ? new CustomerBasket(id) : customerBasket;
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasket basket)
        {
            var customerBasket = await _repo.UpdateBasketAsync(basket);
            if (customerBasket is null)
            {
                //in this case it's a front issue because in our case it the basket foudned it will be update 
                //else it will be created from scratch
                return BadRequest(new ApiBaseError(400));
            }
            else
            {
                return customerBasket;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteCustomerBasket(string id)
        {
            return await _repo.DeleteBasket(id);
        }
    }
}
