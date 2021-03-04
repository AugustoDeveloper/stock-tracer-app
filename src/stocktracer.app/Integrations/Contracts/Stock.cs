using System.Text.Json.Serialization;

namespace stocktracer.app.Integrations.Contracts
{
    public class Stock
    {
        [JsonPropertyName("chart")]
        public Chart Chart { get; set; }
    }
}