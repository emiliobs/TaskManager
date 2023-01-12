using TaskManagerRepositories.Data;
using TaskManagerRepositories.Insfractructure;

namespace TaskManagerRepositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            var repo = new GenericRepository<T>(_context);

            return repo;
        }

        public void Save()
        {
            _context.SaveChanges(); 
        }
    }
}
