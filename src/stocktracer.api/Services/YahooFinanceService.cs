using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Stocktracer.Api.Services.Contracts;

namespace Stocktracer.Api.Services
{
    public class YahooFinanceService : IYahooFinanceService
    {
        private readonly HttpClient client;

        public YahooFinanceService(HttpClient client)
        {
            this.client = client;
        }
        
        Task<Stock> IYahooFinanceService.GetStockFinanceAsync(string symbol) => client.GetFromJsonAsync<Stock>(symbol);
    }
}