using System.Text.Json.Serialization;

namespace Stocktracer.Api.Services.Contracts
{
    public class Quote
    {
        [JsonPropertyName("high")]
        public decimal?[] Highs { get; set; }

        [JsonPropertyName("low")]
        public decimal?[] Lows { get; set; }

        [JsonPropertyName("open")]
        public decimal?[] Opens { get; set; }

        [JsonPropertyName("close")]
        public decimal?[] Closes { get; set; }
    }
}