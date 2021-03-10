using System;
using Stocktracer.Api.Utils;

namespace Stocktracer.Api.Models
{
    public struct SymbolIndicatorValue
    {
        public double IndicatorTimestamp { get; }
        public DateTime IndicatorTime => IndicatorTimestamp.ToDatetime();
        public decimal? Open { get; }
        public decimal? High { get; }
        public decimal? Low { get; }
        public decimal? Close { get; }

        public SymbolIndicatorValue(double timestamp, decimal? open, decimal? high, decimal? low, decimal? close)
        {
            this.IndicatorTimestamp = timestamp;
            this.Open = open;
            this.High = high;
            this.Low = low;
            this.Close = close;
        }
    }
}