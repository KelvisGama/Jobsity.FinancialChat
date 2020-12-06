using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize);
        Task<TEntity> CreateAsync(TEntity obj);
        Task UpdateAsync(string id, TEntity obj);
        Task RemoveAsync(string id);
    }
}
