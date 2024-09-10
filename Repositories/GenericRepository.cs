using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories
{
    public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = _context;
        private readonly DbSet<T> _dbset = _context.Set<T>();
        public async ValueTask AddAsync(T model) => await _dbset.AddAsync(model);

        public void Delete(T model) => _dbset.Remove(model);

        public IQueryable<T> GetAll() => _dbset.AsQueryable().AsNoTracking();

        public ValueTask<T?> GetByIdAsync(int id) => _dbset.FindAsync(id);

        public void Update(T model) => _dbset.Update(model);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
            => _dbset.Where(predicate).AsQueryable().AsNoTracking();

        public async ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate)
            =>await _dbset.AnyAsync(predicate);
    }
}
