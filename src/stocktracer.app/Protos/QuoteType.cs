
using ProtoBuf;

namespace stocktracer.app.Protos
{
    public partial class Yaticker
    {
        [ProtoContract()]
        public enum QuoteType
        {
            [ProtoEnum(Name = @"NONE")]
            None = 0,
            [ProtoEnum(Name = @"ALTSYMBOL")]
            Altsymbol = 5,
            [ProtoEnum(Name = @"HEARTBEAT")]
            Heartbeat = 7,
            [ProtoEnum(Name = @"EQUITY")]
            Equity = 8,
            [ProtoEnum(Name = @"INDEX")]
            Index = 9,
            [ProtoEnum(Name = @"MUTUALFUND")]
            Mutualfund = 11,
            [ProtoEnum(Name = @"MONEYMARKET")]
            Moneymarket = 12,
            [ProtoEnum(Name = @"OPTION")]
            Option = 13,
            [ProtoEnum(Name = @"CURRENCY")]
            Currency = 14,
            [ProtoEnum(Name = @"WARRANT")]
            Warrant = 15,
            [ProtoEnum(Name = @"BOND")]
            Bond = 17,
            [ProtoEnum(Name = @"FUTURE")]
            Future = 18,
            [ProtoEnum(Name = @"ETF")]
            Etf = 20,
            [ProtoEnum(Name = @"COMMODITY")]
            Commodity = 23,
            [ProtoEnum(Name = @"ECNQUOTE")]
            Ecnquote = 28,
            [ProtoEnum(Name = @"CRYPTOCURRENCY")]
            Cryptocurrency = 41,
            [ProtoEnum(Name = @"INDICATOR")]
            Indicator = 42,
            [ProtoEnum(Name = @"INDUSTRY")]
            Industry = 1000,
        }
    }
}