using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniversityScheduler.Api.Infrastructure.Utils;

[Obsolete]
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "dd/MM/yyyy";

    //this method reads and converts the JSON into DateOnly
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var content = reader.GetString();

        if (DateOnly.TryParseExact(content, Format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dateOnly))
            return dateOnly;

        throw new JsonException($"Invalid date format: {content}");
    }

    //this method writes a DateOnly value as a JSON
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}