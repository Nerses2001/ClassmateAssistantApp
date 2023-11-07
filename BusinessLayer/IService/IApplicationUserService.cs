
using Model;

namespace BusinessLayer.IService
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDTO> GetUserByIdAsync(string userId);
        Task<ApplicationUserDTO> GetUserByEmailAsync(string email);
        Task CreateUserAsync(ApplicationUserDTO user);
        Task UpdateUserAsync(ApplicationUserDTO user);
        Task DeleteUserAsync(string userId);
        Task DeleteUserByEamilAsync(string email);
        ApplicationUserDTO GetUserById(string userId);
        ApplicationUserDTO GetUserByEmail(string email);
        void DeleteUserById(string userId);
        void DeleteUserByEamil(string email);
        Task<ICollection<ApplicationUserDTO>> GetAllUsersAsync();


    }
}
