using System.ComponentModel;

namespace Api.Core.Entites.Identity
{
    public class UserDto
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Token { get; set; }


    }
}