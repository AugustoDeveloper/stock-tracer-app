using System.Text.Json.Serialization;

namespace Stocktracer.Api.Services.Contracts
{
    public class Chart
    {
        [JsonPropertyName("result")]
        public Result[] Results { get; set; }   
    }
}