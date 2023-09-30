using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;


[JsonConverter(typeof(EmptyStringToEnumConverter<ChangeTypeEnum>))]
internal enum ChangeTypeEnum {
    DELTA,
    SUB_IMAGE,
    RESUB_DELTA,
    HEARTBEAT
}
