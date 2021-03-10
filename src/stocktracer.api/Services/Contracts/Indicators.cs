using System.Text.Json.Serialization;

namespace Stocktracer.Api.Services.Contracts
{
    public class Indicators
    {
        [JsonPropertyName("quote")]
        public Quote[] Quotes { get; set; }
    }
}