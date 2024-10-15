using Api.Core.Entites.Identity;
using Api.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> userManger;
        private readonly ITokenService token;

        public AccountController(UserManager<AppUser> userManger, ITokenService token)
        {
            this.userManger = userManger;
            this.token = token;
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


        //Login endpoint
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUser login)
        {
            //validate if this user is founded or not 
            var user = await userManger.FindByEmailAsync(login.Email);
            if (user is not null)
            {
                //validate password
                var isPasswordCorrect = await userManger.CheckPasswordAsync(user, login.Password);

                if (isPasswordCorrect)
                {
                    return new UserDto()
                    {
                        DisplayName = user.UserName,
                        Email = user.Email,
                        Token = await token.GenerateToken(user)
                    };
                }

            }
            return BadRequest(new ApiBaseError(401));
        }
    }
}