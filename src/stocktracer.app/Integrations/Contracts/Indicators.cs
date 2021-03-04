using System.Text.Json.Serialization;

namespace stocktracer.app.Integrations.Contracts
{
    public class Indicators
    {
        [JsonPropertyName("quote")]
        public Quote[] Quotes { get; set; }
    }
}