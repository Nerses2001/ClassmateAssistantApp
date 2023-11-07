using DataLayer.IRepository;
using System.Linq.Expressions;

namespace DataLayer.Repository
{
    public class RepositoryBase<TEntity>
        : IRepositoryBase<TEntity> where TEntity : class
    {
        #region Variables

        public readonly ApplicationContext _context;

        #endregion

        #region Constructor

        public RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        /// <summary>  
        /// Adds the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            SaveRepositoryChanges();
        }

        /// <summary>  
        /// Adds the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            SaveRepositoryChanges();
        }

        /// <summary>  
        /// Finds the specified predicate.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _context.Set<TEntity>().Where(predicate);

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().SingleOrDefault(predicate) ?? throw new Exception("No entity found.");
        }
        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        public TEntity FirstOrDefault()
        {
            return _context.Set<TEntity>().SingleOrDefault() ?? throw new Exception("No entity found.");

        }

        /// <summary>  
        /// Gets the specified identifier.  
        /// </summary>  
        /// <param name="id">The identifier.</param>  
        /// <returns></returns>  
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id) ?? throw new Exception("No entity found.");
        }

        /// <summary>  
        /// Gets the specified identifier async.  
        /// </summary>  
        /// <param name="id">The identifier.</param>  
        /// <returns></returns> 
        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id) ?? throw new Exception("No entity found.");
        }

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>  
        /// Removes the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            SaveRepositoryChanges();
        }

        /// <summary>  
        /// Removes the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            SaveRepositoryChanges();
        }

        /// <summary>
        /// Updates the specified entity.  
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            SaveRepositoryChanges();
        }

        /// <summary>
        /// Updates the range.  
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            SaveRepositoryChanges();
        }

        /// <summary>
        /// Asynchronously saves changes made in the repository.
        /// </summary>
        public async Task SaveRepositoryChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Saves changes made in the repository.
        /// </summary>
        public void SaveRepositoryChanges()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}