using AutoMapper;
using BusinessLayer.IService;
using DataLayer.IRepository;
using Entity;
using Model;

namespace BusinessLayer.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _repository;
        private readonly IMapper _mapper;
        public ApplicationUserService(IApplicationUserRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateUserAsync(ApplicationUserDTO user)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            await _repository.CreateUserAsync(appUser);
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _repository.DeleteUserAsync(userId);
        }

        public void DeleteUserByEamil(string email)
        {
            _repository.DeleteUserByEamil(email);
        }

        public async Task DeleteUserByEamilAsync(string email)
        {
            await _repository.DeleteUserByEamilAsync(email);
        }

        public void DeleteUserById(string userId)
        {
            _repository.DeleteUserById(userId);
        }

        public async Task<ICollection<ApplicationUserDTO>> GetAllUsersAsync()
        {
            var appUsers = await _repository.GetAllUsersAsync();
            return _mapper.Map<ICollection<ApplicationUserDTO>>(appUsers);
        }

        public ApplicationUserDTO GetUserByEmail(string email)
        {
            var appUser = _repository.GetUserByEmail(email);
            return _mapper.Map<ApplicationUserDTO>(appUser);
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string email)
        {
            var appUser = await _repository.GetUserByEmailAsync(email);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public ApplicationUserDTO GetUserById(string userId)
        {
            var appUser = _repository.GetUserById(userId);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public async Task<ApplicationUserDTO> GetUserByIdAsync(string userId)
        {
            var appUser = await _repository.GetUserByIdAsync(userId);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public async Task UpdateUserAsync(ApplicationUserDTO user)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            await _repository.UpdateUserAsync(appUser);
        }
    }
}
