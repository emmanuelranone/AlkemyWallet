using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        JwtSecurityToken GetToken(List<Claim> claims);
    }
}
