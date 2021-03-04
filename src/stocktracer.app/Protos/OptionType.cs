
using ProtoBuf;

namespace stocktracer.app.Protos
{
    public partial class Yaticker
    {
        [ProtoContract()]
        public enum OptionType
        {
            [ProtoEnum(Name = @"CALL")]
            Call = 0,
            [ProtoEnum(Name = @"PUT")]
            Put = 1,
        }
    }
}