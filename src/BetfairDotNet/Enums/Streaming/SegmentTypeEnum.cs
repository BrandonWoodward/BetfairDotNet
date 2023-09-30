using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;


[JsonConverter(typeof(EmptyStringToEnumConverter<SegmentTypeEnum>))]
internal enum SegmentTypeEnum {
    NONE,
    SEG_START,
    SEG,
    SEG_END
}
