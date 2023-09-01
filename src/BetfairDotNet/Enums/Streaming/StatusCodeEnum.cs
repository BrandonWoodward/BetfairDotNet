using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;


[JsonConverter(typeof(CustomStringToEnumConverter<StatusCodeEnum>))]
internal enum StatusCodeEnum {

    SUCCESS,
    FAILURE
}
