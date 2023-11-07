using Entity;

namespace DataLayer.IRepository
{
    public interface IUserCourseRepository : 
        IRepositoryBase<UserCourse>
    {
        UserCourse GetById(string userId, int courseId);
        Task<UserCourse> GetByIdAsync(string userId, int courseId);
        Task AddAsync(UserCourse course);
        Task UpdateAsync(UserCourse course);
        Task DeleteAsync(string userId, int courseId);
        Task<ICollection<UserCourse>> GetAllCourcesByUserIdAsync(string userId);
        Task<ICollection<UserCourse>> GetAllUserByCourceIdAsync(int courceId);
        Task<IEnumerable<UserCourse>> GetAllUserCources();

    }
}
