using eintech.api.Models;
using eintech.domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eintech.api.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Delete(Guid id);

        Task SaveAsync();
    }


    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            DbSet<TEntity> dbSet = _dbContext.Set<TEntity>();

            return dbSet.ToList();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>();

            _dbContext.Entry(entity).State = EntityState.Modified;

            await SaveAsync();

            return entity;
        }

        public virtual async Task<TEntity> Delete(Guid id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            _dbContext.Set<TEntity>().Remove(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        public virtual async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
