using Microsoft.EntityFrameworkCore;
using MovieMonsterApi.Data;
using System.Linq.Expressions;

namespace MovieMonsterApi.Repositories
{

    public interface IEntity
    {

    }
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

    
    public abstract class Entity<TId> : IEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
    public interface IEFBaseAsyncRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);

    }
    public class EFBaseAsyncRepository<TEntity, TId> : IEFBaseAsyncRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private readonly MovieMonsterContext _context;
        internal DbSet<TEntity> dbSet;

        public EFBaseAsyncRepository(MovieMonsterContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await dbSet.FindAsync(id);
        }

        //public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = Including(includeProperties);
            
        //    return await query.;
        //}

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //Generisk metod för att inkludera relaterade tabeller
        private IQueryable<TEntity> Including(params Expression<Func<TEntity, object>> [] includeProperties)
        {
            return includeProperties.Aggregate(dbSet.AsQueryable(), (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
