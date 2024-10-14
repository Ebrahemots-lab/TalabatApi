using Microsoft.AspNetCore.Identity;

namespace Api.Core.Entites.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }

    public Adress Adress { get; set; }


}
