using System;
using stocktracer.app.Utils;

namespace stocktracer.app.Models
{
    public class SymbolIndicatorValue
    {
        public double IndicatorTimestamp { get; set; }
        public DateTime IndicatorTime { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
    }
}