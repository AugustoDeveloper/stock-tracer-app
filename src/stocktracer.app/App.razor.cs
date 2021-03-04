using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using stocktracer.app.Protos;
using  Microsoft.AspNetCore.WebUtilities;
using stocktracer.app.Integrations;
using stocktracer.app.Integrations.Contracts;
using stocktracer.app.Models;
using System.Collections.Generic;

namespace stocktracer.app
{
    public partial class App
    {

        [Inject]
        private IJSRuntime CurrentRuntime { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private CancellationTokenSource cancellationTokenSource;
        private WebSocketIntegrationClient clientSocket;
        private List<StockTrack> stocks = new List<StockTrack>();

        async protected override Task OnInitializedAsync()
        {
            cancellationTokenSource = new CancellationTokenSource();
            var symbols = new[] { "GME" };

            stocks.AddRange(symbols.Select(symbol => new StockTrack { Symbol = symbol }));

            clientSocket = new WebSocketIntegrationClient(
                new ClientWebSocket(), 
                new Uri("wss://streamer.finance.yahoo.com"), 
                cancellationTokenSource.Token);

            clientSocket.OnReceiveMessage += ExecuteReceivingMessage;
            clientSocket.OnConnected += ExecuteWebSocketConnected;
            await clientSocket.ConnectAsync();

            await base.OnInitializedAsync();
        }

        private void ExecuteWebSocketConnected()
        {
            try
            {
                var subscription = new 
                {
                    subscribe = this.stocks.Select(x => x.Symbol).ToArray()
                };

                Console.WriteLine("Connected");

                var payload = JsonSerializer.Serialize(subscription);
                this.clientSocket.SendStringAsync(payload, default);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Connected - Error");
                Console.WriteLine(ex);
            }
        }

        private void ExecuteReceivingMessage(byte[] message)
        {
            try 
            {
                var textMessage = Encoding.UTF8.GetString(message);
                var textMessageFromBase64 = Convert.FromBase64String(textMessage);

                using var stream = new MemoryStream(textMessageFromBase64);
                var ticker = ProtoBuf.Serializer.Deserialize<Yaticker>(stream);

                var stonk = stocks.First(s => s.Symbol == ticker.Id);
                stonk.IsDown = stonk.Price < ticker.Price;

                if (ticker.Price != stonk.Price)
                {
                    stonk.Price = ticker.Price;
                    StateHasChanged();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Receiving - Error");
                Console.WriteLine(ex);
            }
        }

        private void RemoveStockBySymbol(string symbol)
        {
            this.stocks.RemoveAll(s => string.Equals(s.Symbol, symbol, StringComparison.InvariantCultureIgnoreCase));
        }

        private void AddSymbol(string symbol)
        {
            if (!this.stocks.Any(s => string.Equals(s.Symbol, symbol, StringComparison.InvariantCultureIgnoreCase)))
                this.stocks.Add(new StockTrack
                {
                    Symbol = symbol
                });
        }
    }
}