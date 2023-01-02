using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllDtoAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);

        }

        public PagedList<UserListDTO> GetAllPage(int page = 1)
        {
            if (page < 1) throw new ArgumentException("Argument must be greater than 0", "page");

            var users = _unitOfWork.UserRepository.GetAllPaged(page,10);
            
            var usersDTO = _mapper.Map<List<UserListDTO>>(users);

            var pagedUsers = new PagedList<UserListDTO>(usersDTO, users.TotalCount, page);
            return pagedUsers;
        }

    }
}
