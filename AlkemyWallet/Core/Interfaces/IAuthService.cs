using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
    }
}
