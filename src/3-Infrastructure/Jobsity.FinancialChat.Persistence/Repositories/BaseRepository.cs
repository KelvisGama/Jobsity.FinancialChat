using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<TEntity> GetByIdAsync(string id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task DeleteAsync(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            _context.Set<TEntity>().Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
