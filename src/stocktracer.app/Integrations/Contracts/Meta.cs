using System.Text.Json.Serialization;

namespace stocktracer.app.Integrations.Contracts
{
    public class Meta
    {
        [JsonPropertyName("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [JsonPropertyName("firstTradeDate")]
        public double FirstTradeDate { get; set; }

        [JsonPropertyName("regularMarketTime")]
        public double RegularMarketTime { get; set; }
    }
}