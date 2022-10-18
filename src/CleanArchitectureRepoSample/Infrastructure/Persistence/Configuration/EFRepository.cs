using Application.Common.Interface;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Configuration
{
    internal class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        protected DbSet<TEntity> _entities;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;

            _entities = _context.Set<TEntity>();
        }


        public virtual IQueryable<TEntity> Table => _entities.AsQueryable();

        public virtual IQueryable<TEntity> TableNoTracking => _entities.AsNoTracking();

        public virtual void Add(TEntity entity)
        {
            try
            {
                _entities.AddAsync(entity);
            }
            catch (Exception)
            {
                //
            }
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await _entities.Where(where).CountAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public virtual async Task<int> Count()
        {
            try
            {
                return await _entities.CountAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public virtual async Task<bool> Delete(object id)
        {
            try
            {
                var entity = await _entities.FindAsync(id);

                _entities.Remove(entity);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual Task<bool> Delete(TEntity entity)
        {
            try
            {
                _entities.Remove(entity);

                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public virtual Task<bool> Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = _entities.Where(where);

                _entities.RemoveRange(entities);

                return Task.FromResult(true);
            }
            catch(Exception)
            {
                return Task.FromResult(false);
            }
        }

        public virtual async Task<TEntity> Get(object id)
        {
            try
            {
                return await _entities.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await _entities.FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await _entities.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = await _entities.Where(where).ToListAsync();

                return entities;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _entities.Update(entity);
            }
            catch (Exception)
            {

            }
        }
    }
}
