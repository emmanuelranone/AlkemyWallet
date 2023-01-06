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
        
        public async Task<BriefUserDTO> Register(RegisterDTO newUser)
        {
            var validateUser = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(u => u.Email == newUser.Email, null, "");

            if (validateUser != null)
            {
                return null;
            }
            else
            {
                //var encryptedPassword = AuthHelper.EncryptPassword(newUser.Password);

                var user = new User
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Password = newUser.Password,
                    Points = 1,
                    RoleId = 2,
                    IsDeleted = false
                };

                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<BriefUserDTO>(user);
            }
        }

        public async Task<int> Delete(int id)
        {
            var deleted = await _unitOfWork.UserRepository.Delete(id);
            if(deleted != 0) 
            { 
                _unitOfWork.SaveChanges();
                return id;
            }else{
                return 0;
            }

            
        }

        public async Task<UserGetByIdDTO> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            return _mapper.Map<UserGetByIdDTO>(user);
        }

        public async Task<User> UpdateAsync(int id, UserUpdateDTO dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

                user = UserMapper.UserUpdateDtoToUser(dto, user);

                user = await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
