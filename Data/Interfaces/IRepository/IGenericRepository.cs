using System.Linq.Expressions;

namespace Data.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        Task<T> GetFirst(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Remove(T entity);
    }
}
