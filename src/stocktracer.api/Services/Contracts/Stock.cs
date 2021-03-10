using System.Text.Json.Serialization;

namespace Stocktracer.Api.Services.Contracts
{
    public class Stock
    {
        [JsonPropertyName("chart")]
        public Chart Chart { get; set; }
    }
}