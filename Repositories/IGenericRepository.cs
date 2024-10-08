﻿using System.Linq.Expressions;

namespace App.Repositories
{
    public interface IGenericRepository<T, TId> where T :class where TId : struct
    {
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T model);
        ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        void Update(T model);
        void Delete(T model);

    }
}
