using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<AuthMeDTO> GetAuthMeAsync(int id);
    }
}
