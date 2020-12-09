using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Common.Interfaces.ExternalApis
{
    public interface IStooqApi
    {
        Task<string> GetStockQuote(string stockName);
    }
}