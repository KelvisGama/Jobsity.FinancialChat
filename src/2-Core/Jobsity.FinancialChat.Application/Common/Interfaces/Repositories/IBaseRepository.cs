using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> CreateAsync(TEntity obj);
        Task UpdateAsync(TEntity obj);
        Task DeleteAsync(TEntity obj);
    }
}
