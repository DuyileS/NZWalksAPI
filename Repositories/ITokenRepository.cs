using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
