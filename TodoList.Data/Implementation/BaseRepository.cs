using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoList.Domain.Model;

namespace TodoList.Data
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;

        protected BaseRepository(DbContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where = null) => await _context.Set<TEntity>().Where(where).ToListAsync();

        public async Task<TEntity> GetById(Expression<Func<TEntity, bool>> predicate = null) => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
            {
                throw new ArgumentNullException(nameof(updated));
            }

            return await _updateAsync(updated, key) > 0;
        }

        private async Task<int> _updateAsync(TEntity updated, int key)
        {
            var updatedRows = 0;

            TEntity existing = await Task.Run(() => _context.Set<TEntity>().Find(key));

            if (existing != null)
            {
                updatedRows = await Task.Run(() =>
                {
                    _context.Entry(existing).CurrentValues.SetValues(updated);
                    return _context.SaveChangesAsync();
                });
            }

            return updatedRows;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await _context.AddRangeAsync(entities);

        public async Task<bool> RemoveAsync(int id) 
        {
            var updatedRows = 0;
            TEntity element = await Task.Run(() => _context.Set<TEntity>().Find(id));

            if (element != null)
            {
                updatedRows = await Task.Run(() => 
                {
                    _context.Set<TEntity>().Remove(element);

                    return _context.SaveChangesAsync();
                });
            }

            return updatedRows > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
