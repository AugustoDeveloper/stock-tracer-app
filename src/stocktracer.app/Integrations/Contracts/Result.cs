using System.Text.Json.Serialization;

namespace stocktracer.app.Integrations.Contracts
{
    public class Result
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("timestamp")]
        public double[] Timestamps { get; set; }

        [JsonPropertyName("indicators")]
        public Indicators Indicators { get; set; }
    }
}