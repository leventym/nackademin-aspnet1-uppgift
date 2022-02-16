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
        Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
        Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties);


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

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties)
        {
            var entities = await dbSet.ToListAsync();
            await Including(entities, includeProperties);
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties)
        {
            var entity = await dbSet.FindAsync(id);

            await Including(entity, includeProperties);

            return entity;

        }
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //Generisk metod för att inkludera relaterade tabeller
        private async Task<TEntity> Including(TEntity entity, params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties)
        {

            foreach (var property in includeProperties)
            {
                await _context.Entry(entity).Collection(property).LoadAsync();
            }
            return entity;

        }

        private async Task<IEnumerable<TEntity>> Including(IEnumerable<TEntity> entities, params Expression<Func<TEntity, IEnumerable<object>>>[] includeProperties)
        {
            foreach(var entity in entities)
            {
                foreach (var property in includeProperties)
                {
                    await _context.Entry(entity).Collection(property).LoadAsync();
                }
            }
            return entities;
        }
    }
}
