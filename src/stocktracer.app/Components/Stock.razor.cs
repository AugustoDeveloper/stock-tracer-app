using System;
using System.Collections.ObjectModel;
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

        private string SymbolChartId => $"chart-{this.Data.Symbol.Replace('.', '-')}";
        
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
            var finance = await Http.GetFromJsonAsync<SymbolFinance>("stock/"+Data.Symbol);
            FirstTradeDate = finance.FirstTradeDate;
            RegularMarketTime = finance.RegularMarketTime;

            var dataSerie = this.GenerateDataSerie(finance.Indicators);

            try
            {
                await CurrentRuntime.InvokeAsync<string>(
                    identifier: "apexChart.renderChart", 
                    cancellationToken: default, 
                    args: new object[] 
                    { 
                        Data.Symbol,//stockSymbol
                        $"chart-{Data.Symbol.Replace('.', '-')}", //div chartId 
                        $"chart-{Data.Symbol.Replace('.', '-')}", //svg chart id
                        dataSerie // data
                    } );

                if (Data.Price == -1.0)
                {
                    Data.Price = (float)finance.RegularMarketPrice;
                }
                StateHasChanged();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        async private Task RemoveAsync()
        {
            await this.EventRemoveStock.InvokeAsync(this.Data.Symbol);
        }

        private object[] GenerateDataSerie(Collection<SymbolIndicatorValue> indicators)
            => indicators
                .Select(i => 
                new 
                {
                    x = i.IndicatorTimestamp * 1000,
                    y = new[] 
                    { 
                        i.Open?.ToString("#.##"),
                        i.High?.ToString("#.##"),
                        i.Low?.ToString("#.##"),
                        i.Close?.ToString("#.##")
                    }
                })
                .ToArray();

    }
}