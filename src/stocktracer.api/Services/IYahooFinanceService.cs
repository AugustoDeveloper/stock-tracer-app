using System.Threading.Tasks;
using Stocktracer.Api.Services.Contracts;

namespace Stocktracer.Api.Services
{
    public interface IYahooFinanceService
    {
        Task<Stock> GetStockFinanceAsync(string symbol);
    }
}