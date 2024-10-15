using Api.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;

namespace Api.Data
{
    public static class IdentityUserSeeding
    {
        public async static Task UserSeeding(UserManager<AppUser> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "EbrahemOts",
                    Email = "ebrahemots@gmail.com",
                    UserName = "ebrahemots",
                    PhoneNumber = "1122334455"
                };

                var userCreated = await _userManager.CreateAsync(user, "Password@@##");

            }
        }
    }
}