using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniversityScheduler.Api.Infrastructure.Utils;

[Obsolete]
public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string Format = "hh:mm:ss";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var content = reader.GetString();

        if (TimeOnly.TryParseExact(content, Format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var timeOnly))
            return timeOnly;

        throw new JsonException($"Invalid time format: {content}");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}