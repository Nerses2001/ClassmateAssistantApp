using DataLayer.IRepository;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class UserCourseRepository : 
        RepositoryBase<UserCourse>,
        IUserCourseRepository
    {
        public UserCourseRepository(ApplicationContext context): 
            base(context) { }

        public async Task AddAsync(UserCourse course)
        {
            await _context.UserCourses.AddAsync(course);
            await SaveRepositoryChangesAsync();
        }

        public async Task DeleteAsync(string userId,int courseId)
        {
            var exitCourse = await _context.UserCourses
                .SingleOrDefaultAsync(uc=> uc.CourseId == courseId && uc.UserId == userId);
            
            if (exitCourse != null)
            {
                _context.UserCourses.Remove(exitCourse);
                await SaveRepositoryChangesAsync();
            }
            else
                throw new Exception("UserCource Not Found");
        }

        public async Task<ICollection<UserCourse>> GetAllCourcesByUserIdAsync(string userId)
        {
            var cources = await _context.UserCourses
                .Where(uc => uc.UserId == userId)
                .ToListAsync();

            return cources;
        }

        public async Task<ICollection<UserCourse>> GetAllUserByCourceIdAsync(int courceId)
        {
            var users = await _context.UserCourses
                .Where(uc => uc.CourseId == courceId)
                .ToListAsync();

            return users;

        }

        public async Task<IEnumerable<UserCourse>> GetAllUserCources()
        {
            var userCources = await _context.UserCourses.ToListAsync();
            return userCources;
        }

        public UserCourse GetById(string userId, int courseId)
        {
            var  exitingUser = _context.UserCourses
                .SingleOrDefault(uc => uc.UserId == userId && uc.CourseId == courseId);
            if (exitingUser != null)
                return exitingUser;
            
            else
                throw new Exception("UserCource Not Found");
        }

        public async Task<UserCourse> GetByIdAsync(string userId, int courseId)
        {
            var exitingUser = await _context.UserCourses
                .SingleOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
            
            if (exitingUser != null)
                return exitingUser;

            else
                throw new Exception("UserCource Not Found");
        }

        public async Task UpdateAsync(UserCourse course)
        {
            var exitingUser = await _context.UserCourses.FindAsync(course);

            if (exitingUser != null)
            {
                _context.UserCourses.Update(exitingUser);
                await SaveRepositoryChangesAsync();
            }
            else
                throw new Exception("UserCource Not Found");

        }
    }
}
