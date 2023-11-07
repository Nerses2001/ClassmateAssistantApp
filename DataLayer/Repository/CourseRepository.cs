using DataLayer.IRepository;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class CourseRepository : 
        RepositoryBase<Course>, 
        ICourseRepository
    {
        public CourseRepository(ApplicationContext context) :
            base(context)
        { }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await SaveRepositoryChangesAsync();
        }

        public async Task DeleteAsync(int courseId)
        {
            var exitCource = await _context.Courses
                .SingleOrDefaultAsync(c => c.Id == courseId);
            if (exitCource != null)
            {
                _context.Courses.Remove(exitCource);
            }
            else
                throw new Exception("Cource Not Found");
        }

        public async Task<ICollection<Course>> GetAllAsync()
        {
            var cources = await _context.Courses.ToListAsync();
            return cources;
        }

        public async Task<Course> GetByIdAsync(int courseId)
        {
            
            var exitCource = await _context.Courses.
                SingleOrDefaultAsync(c => c.Id == courseId);
            
            if(exitCource != null)
                return exitCource;
            
            else
                throw new Exception("Cource Not Found");
        }

        public async Task UpdateAsync(Course course)
        {
            var exitCource = await _context.Courses
                .SingleOrDefaultAsync(c => c.Id == course.Id);
            
            if (exitCource != null)
            {
                _context.Update(course);
                SaveRepositoryChanges();
            }
            else throw new Exception("Cource Not Found");
        }
    }
}
