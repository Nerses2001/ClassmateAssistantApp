using Entity;

namespace DataLayer.IRepository
{
    public interface ICourseRepository : 
        IRepositoryBase<Cource>
    {
        Task<Cource> GetByIdAsync(int courseId);
        Task AddAsync(Cource course);
        Task UpdateAsync(Cource course);
        Task DeleteAsync(int courseId);
        Task<ICollection<Cource>> GetAllAsync();

    }
}
