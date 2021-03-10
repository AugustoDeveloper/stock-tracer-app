using System;
using System.Collections.ObjectModel;

namespace stocktracer.app.Models
{
    public class SymbolFinance
    {
        public DateTime FirstTradeDate { get; set; }
        public DateTime RegularMarketTime { get; set; }
        public decimal RegularMarketPrice { get; set; }
        public Collection<SymbolIndicatorValue> Indicators { get; set; }
    }
}