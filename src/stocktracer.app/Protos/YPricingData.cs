
using ProtoBuf;

namespace stocktracer.app.Protos
{

    [ProtoContract(Name = @"yaticker")]
    public partial class Yaticker : IExtensible
    {
        private IExtension __pbn__extensionData;
        IExtension IExtensible.GetExtensionObject(bool createIfMissing)
            => Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [ProtoMember(1, Name = @"id")]
        [System.ComponentModel.DefaultValue("")]
        public string Id { get; set; } = "";

        [ProtoMember(2, Name = @"price")]
        public float Price { get; set; }

        [ProtoMember(3, Name = @"time", DataFormat = DataFormat.ZigZag)]
        public long Time { get; set; }

        [ProtoMember(4, Name = @"currency")]
        [System.ComponentModel.DefaultValue("")]
        public string Currency { get; set; } = "";

        [ProtoMember(5, Name = @"exchange")]
        [System.ComponentModel.DefaultValue("")]
        public string Exchange { get; set; } = "";

        [ProtoMember(6)]
        public QuoteType quoteType { get; set; }

        [ProtoMember(7)]
        public MarketHoursType marketHours { get; set; }

        [ProtoMember(8)]
        public float changePercent { get; set; }

        [ProtoMember(9, DataFormat = DataFormat.ZigZag)]
        public long dayVolume { get; set; }

        [ProtoMember(10)]
        public float dayHigh { get; set; }

        [ProtoMember(11)]
        public float dayLow { get; set; }

        [ProtoMember(12, Name = @"change")]
        public float Change { get; set; }

        [ProtoMember(13)]
        [System.ComponentModel.DefaultValue("")]
        public string shortName { get; set; } = "";

        [ProtoMember(14, DataFormat = DataFormat.ZigZag)]
        public long expireDate { get; set; }

        [ProtoMember(15)]
        public float openPrice { get; set; }

        [ProtoMember(16)]
        public float previousClose { get; set; }

        [ProtoMember(17)]
        public float strikePrice { get; set; }

        [ProtoMember(18)]
        [System.ComponentModel.DefaultValue("")]
        public string underlyingSymbol { get; set; } = "";

        [ProtoMember(19, DataFormat = DataFormat.ZigZag)]
        public long openInterest { get; set; }

        [ProtoMember(20)]
        public OptionType optionsType { get; set; }

        [ProtoMember(21, DataFormat = DataFormat.ZigZag)]
        public long miniOption { get; set; }

        [ProtoMember(22, DataFormat = DataFormat.ZigZag)]
        public long lastSize { get; set; }

        [ProtoMember(23, Name = @"bid")]
        public float Bid { get; set; }

        [ProtoMember(24, DataFormat = DataFormat.ZigZag)]
        public long bidSize { get; set; }

        [ProtoMember(25, Name = @"ask")]
        public float Ask { get; set; }

        [ProtoMember(26, DataFormat = DataFormat.ZigZag)]
        public long askSize { get; set; }

        [ProtoMember(27, DataFormat = DataFormat.ZigZag)]
        public long priceHint { get; set; }

        [ProtoMember(28, Name = @"vol_24hr", DataFormat = DataFormat.ZigZag)]
        public long Vol24hr { get; set; }

        [ProtoMember(29, DataFormat = DataFormat.ZigZag)]
        public long volAllCurrencies { get; set; }

        [ProtoMember(30, Name = @"fromcurrency")]
        [System.ComponentModel.DefaultValue("")]
        public string Fromcurrency { get; set; } = "";

        [ProtoMember(31)]
        [System.ComponentModel.DefaultValue("")]
        public string lastMarket { get; set; } = "";

        [ProtoMember(32)]
        public double circulatingSupply { get; set; }

        [ProtoMember(33, Name = @"marketcap")]
        public double Marketcap { get; set; }
    }
}