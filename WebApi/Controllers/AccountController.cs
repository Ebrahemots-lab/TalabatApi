using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManger;

        public AccountController(UserManager<AppUser> userManger)
        {
            this.userManger = userManger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Regiester(RegiesterUser user)
        {
            var databaseUser = new AppUser()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                UserName = user.Email.Split('@')[0],
                PhoneNumber = user.PhoneNumber,
            };

            //create the user
            var isUserCreated = await userManger.CreateAsync(databaseUser, user.Password);
            if (isUserCreated.Succeeded)
            {
                return new UserDto()
                {
                    Email = databaseUser.Email,
                    DisplayName = databaseUser.DisplayName,
                    Token = "ThisIsToken",
                };
            }

            return BadRequest(new ApiBaseError(400));

        }
    }
}