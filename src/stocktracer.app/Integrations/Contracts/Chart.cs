using System.Text.Json.Serialization;

namespace stocktracer.app.Integrations.Contracts
{
    public class Chart
    {
        [JsonPropertyName("result")]
        public Result[] Results { get; set; }   
    }
}