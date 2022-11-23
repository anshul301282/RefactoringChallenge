using Microsoft.EntityFrameworkCore;
using RefactoringChallenge.Data;
using RefactoringChallenge.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RefactoringChallenge.Repositories
{
    public class RepositoryBase<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly NorthwindDbContext _northwindDbContext;

        public RepositoryBase(NorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext ?? throw new ArgumentNullException(nameof(northwindDbContext));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _northwindDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _northwindDbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _northwindDbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _northwindDbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _northwindDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _northwindDbContext.Set<T>().Add(entity);
            await _northwindDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _northwindDbContext.Attach(entity);
            _northwindDbContext.Entry(entity).State = EntityState.Modified;
            await _northwindDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _northwindDbContext.Set<T>().Remove(entity);
            await _northwindDbContext.SaveChangesAsync();
        }
    }
}
