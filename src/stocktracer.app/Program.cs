using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace stocktracer.app
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => 
            {
                var http = new HttpClient 
                { 
                    BaseAddress = new Uri("https://cors.bridged.cc/https://query1.finance.yahoo.com/v8/finance/chart/"),
                }; 

                http.DefaultRequestHeaders.TryAddWithoutValidation("Origin", "bridged.xyz");
                return http;
            });

            await builder.Build().RunAsync();
        }
    }
}
