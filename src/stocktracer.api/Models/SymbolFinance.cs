using System;
using System.Collections.ObjectModel;

namespace Stocktracer.Api.Models
{
    public class SymbolFinance
    {
        public DateTime FirstTradeDate { get; set; }
        public DateTime RegularMarketTime { get; set; }
        public decimal RegularMarketPrice { get; set; }
        public ReadOnlyCollection<SymbolIndicatorValue> Indicators { get; set; }
    }
}