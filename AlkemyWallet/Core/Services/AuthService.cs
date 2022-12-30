using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlkemyWallet.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        
        public async Task<string> Login(string email, string password)
        {
            //Validading credentials
            var user = await _unitOfWork.UserRepository
                .GetFirstOrDefaultAsync(
                u => u.Email == email && 
                u.Password == password, null, "Role");
            
            //JsonWebToken Instance
            var jwt = new JsonWebToken(_configuration);

            //If user is not null, create token.    
            var token = jwt.CreateToken(user);

            //Returning token to controller
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }
    }
}
