using BetfairDotNet.Converters;
using System.Text;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ConverterTests;


public class DoubleNaNToNullConverterTests {


    private double? ReadFromJson(string json) {
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json), isFinalBlock: true, state: default);
        reader.Read();
        var converter = new DoubleNaNToNullConverter();
        return converter.Read(ref reader, typeof(double?), new JsonSerializerOptions());
    }


    [Fact]
    public void Read_Should_Return_Null_For_NaN() {
        var value = ReadFromJson("\"NaN\"");
        Assert.Null(value);
    }


    [Fact]
    public void Read_Should_Return_Number_For_Numeric_String() {
        var value = ReadFromJson("\"42.42\"");
        Assert.Equal(42.42, value);
    }


    [Fact]
    public void Read_Should_Return_Null_For_Null_Token() {
        var value = ReadFromJson("null");
        Assert.Null(value);
    }


    [Fact]
    public void Read_Should_Throw_Exception_For_Invalid_String() {
        Assert.Throws<JsonException>(() => ReadFromJson("\"invalid\""));
    }


    [Fact]
    public void Read_Should_Throw_Exception_For_Unexpected_Token() {
        Assert.Throws<JsonException>(() => ReadFromJson("{ }"));
    }
}
