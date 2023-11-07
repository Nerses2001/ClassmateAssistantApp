using Entity;
using System.Threading.Tasks;

namespace DataLayer.IRepository
{
    public interface IApplicationUserRepository : 
        IRepositoryBase<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task CreateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(string userId);
        Task DeleteUserByEamilAsync(string email);
        ApplicationUser GetUserById(string userId);
        ApplicationUser GetUserByEmail(string email);
        void DeleteUserById(string userId);
        void DeleteUserByEamil(string email);
        Task<ICollection<ApplicationUser>> GetAllUsersAsync();


    }
}
