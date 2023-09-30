using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;


[JsonConverter(typeof(EmptyStringToEnumConverter<StatusCodeEnum>))]
internal enum StatusCodeEnum {

    SUCCESS,
    FAILURE
}
