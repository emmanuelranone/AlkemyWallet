using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<AuthMeDTO> GetAuthMeAsync(int id);
    }
}
