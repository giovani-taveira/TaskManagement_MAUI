using System.Linq.Expressions;
using TaskManagement.MVVM.Models;

namespace TaskManagement.Persistence.Respositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<int> DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
