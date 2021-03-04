
using ProtoBuf;

namespace stocktracer.app.Protos
{
    public partial class Yaticker
    {
        [ProtoContract()]
        public enum MarketHoursType
        {
            [ProtoEnum(Name = @"PRE_MARKET")]
            PreMarket = 0,
            [ProtoEnum(Name = @"REGULAR_MARKET")]
            RegularMarket = 1,
            [ProtoEnum(Name = @"POST_MARKET")]
            PostMarket = 2,
            [ProtoEnum(Name = @"EXTENDED_HOURS_MARKET")]
            ExtendedHoursMarket = 3,
        }

    }
}