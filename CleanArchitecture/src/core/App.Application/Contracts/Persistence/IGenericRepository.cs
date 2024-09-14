using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T model);
        ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        void Update(T model);
        void Delete(T model);

    }
}
