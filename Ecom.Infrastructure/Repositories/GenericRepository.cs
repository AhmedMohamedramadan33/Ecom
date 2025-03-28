using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecom.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepositories<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
        }

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
