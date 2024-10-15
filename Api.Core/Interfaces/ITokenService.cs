using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Entites.Identity;

namespace Api.Core.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(AppUser user);
    }
}