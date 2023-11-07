using DataLayer.IRepository;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class ApplicationUserRepository : 
        RepositoryBase<ApplicationUser>, 
        IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationContext context): 
            base(context){}

        public async Task CreateUserAsync(ApplicationUser user)
        {
            await _context.ApplicationUser.AddAsync(user);
            await SaveRepositoryChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(u=>u.Id == userId);
            
            if(user != null)
            {
                _context.ApplicationUser.Remove(user);
                await SaveRepositoryChangesAsync();
            }
            else
                throw new Exception("User not found.");

        }

        public void DeleteUserByEamil(string email)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.Email == email);
            
            if (user != null)
            {
                _context.ApplicationUser.Remove(user);
                SaveRepositoryChanges();
            }
            else
                throw new Exception("User not found.");
        }

        public async Task DeleteUserByEamilAsync(string email)
        {
            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                _context.ApplicationUser.Remove(user);
                await SaveRepositoryChangesAsync();
            }
            else
                throw new Exception("User not found.");
        }

        public void DeleteUserById(string userId)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.ApplicationUser.Remove(user);
                SaveRepositoryChanges();
            }
            else
                throw new Exception("User not found.");
        }

        public async Task<ICollection<ApplicationUser>> GetAllUsersAsync()
        {
            var users = await _context.ApplicationUser.ToListAsync();
            return users;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.Email == email);

            if (user != null)
                return user;
            else
                throw new Exception("User not found.");
        }
    

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.Email == email);
            
            if (user != null)
                return user;
            else
                throw new Exception("User not found.");
        }

        public ApplicationUser GetUserById(string userId)
        {
            var user = _context.ApplicationUser
                .FirstOrDefault(u => u.Id == userId);
            if (user != null)
                return user;
            else
                throw new Exception("User not found.");

        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user != null)
                return user;
            else
                throw new Exception("User not found.");

        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            var exitedUser = await _context.ApplicationUser.
                FirstOrDefaultAsync(u => u.Email == user.Email);
            
            if (user != null)
            {    
                _context.ApplicationUser.Update(user);
                await SaveRepositoryChangesAsync();
            }
            else
                throw new Exception("User not found.");
            
        }
    }
}
