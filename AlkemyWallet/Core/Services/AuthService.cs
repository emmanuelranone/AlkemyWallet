using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Core.Helper;
using System.IdentityModel.Tokens.Jwt;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
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

        public async Task<AuthMeDTO> GetAuthMeAsync(int id)
        {
            var user = await _unitOfWork.UserRepository
                .GetFirstOrDefaultAsync(u => u.Id == id, null, "Role");

            return UserMapper.UserToAuthMeDTO(user);
        }
    }
}
