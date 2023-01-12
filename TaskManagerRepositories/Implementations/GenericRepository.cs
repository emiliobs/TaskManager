using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TaskManagerRepositories.Data;
using TaskManagerRepositories.Insfractructure;

namespace TaskManagerRepositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context )
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add( entity );
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);

            return entity;
        }

        public void DeleteRange(List<T> entityList)
        {
            _dbSet.RemoveRange(entityList);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disabledTracking = true)
        {
           IQueryable<T> query = _dbSet;
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }
            
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public T GetById(object id)
        {
            return _dbSet.Find(id); 
                
        }

        public T GetByIdAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = include(query);
            }
            
            return query.FirstOrDefault();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State |= EntityState.Modified;

            return entity;
        }
    }
}
