using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using stocktracer.app.Models;
using stocktracer.app.Utils;

namespace stocktracer.app.Components
{
    public partial class Stock
    {
        [Parameter]
        public EventCallback<string> EventRemoveStock { get; set; }

        [Parameter]
        public StockTrack Data { get; set; }

        [Inject]
        private IJSRuntime CurrentRuntime { get; set; }
        
        [Inject]
        private HttpClient Http { get; set; }

        private string SymbolChart => $"chart-{this.Data.Symbol}";
        
        private DateTime LastUpdateChart { get; set; } = DateTime.MinValue;

        private DateTime FirstTradeDate { get; set; }
        private DateTime RegularMarketTime { get; set; }
        public string PriceColorClass => Data.IsDown ? "price-red" : "price-green";
        private int TimeToUpdateInSeconds { get; set; } = 1;
        private string CurrentPriceFormatted => Data.Price.ToString("#.##");

        private Timer updateChartTimer;

        protected override Task OnInitializedAsync()
        {
            updateChartTimer = new Timer((state) =>
            {
                TimeToUpdateInSeconds--;
                if (TimeToUpdateInSeconds == 0)
                {
                    var task = UpdateChartAsync();

                    LastUpdateChart = DateTime.Now;
                    updateChartTimer.Change(0, 1000);
                    TimeToUpdateInSeconds = 600;
                }

                StateHasChanged();
            }, new AutoResetEvent(false), 0, 1000);

            return base.OnInitializedAsync();
        }

        async private Task ForceUpdateChartAsync()
        {
            TimeToUpdateInSeconds = 600;
            await UpdateChartAsync();
        }

        async private Task UpdateChartAsync()
        {
            var stonk = await Http.GetFromJsonAsync<stocktracer.app.Integrations.Contracts.Stock>(Data.Symbol);
            var meta = stonk.Chart.Results.First().Meta;
            FirstTradeDate = meta.FirstTradeDate.ToDatetime();
            RegularMarketTime = meta.RegularMarketTime.ToDatetime();

            var dataSerie = this.GenerateDataSerie(stonk.Chart.Results.First());

            await CurrentRuntime.InvokeAsync<string>("apexChart.renderChart", default, new object[] { Data.Symbol, $"chart-{Data.Symbol}", $"chart-{Data.Symbol}", dataSerie } );

            if (Data.Price == -1)
            {
                Data.Price = (float)meta.RegularMarketPrice;
                StateHasChanged();
            }
        }

        async private Task RemoveAsync()
        {
            await this.EventRemoveStock.InvokeAsync(this.Data.Symbol);
        }

        private object[] GenerateDataSerie(stocktracer.app.Integrations.Contracts.Result result)
            => result.Timestamps
                .Select((t, i) => 
                new 
                {
                    x = t * 1000,
                    y = new[] 
                    { 
                        result.Indicators.Quotes.First().Opens[i]?.ToString("#.##"),
                        result.Indicators.Quotes.First().Highs[i]?.ToString("#.##"),
                        result.Indicators.Quotes.First().Lows[i]?.ToString("#.##"),
                        result.Indicators.Quotes.First().Closes[i]?.ToString("#.##"),
                    }

                })
                .ToArray();

    }
}