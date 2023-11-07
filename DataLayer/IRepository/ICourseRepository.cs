using Entity;

namespace DataLayer.IRepository
{
    public interface ICourseRepository : 
        IRepositoryBase<Course>
    {
        Task<Course> GetByIdAsync(int courseId);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int courseId);
        Task<ICollection<Course>> GetAllAsync();

    }
}
