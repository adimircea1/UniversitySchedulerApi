using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UniversityScheduler.Api.Infrastructure.DateOnlyAndTimeOnlySupport;

public class TimeOnlyConverter : ValueConverter<TimeOnly, string>
{
    public TimeOnlyConverter() : base(time => time.ToString(),
        content => StringToTimeOnly(content))
    {
    }

    private static TimeOnly StringToTimeOnly(string content)
    {
        DateTime.TryParse(content, out var dateTime);

        var timeOnly = TimeOnly.FromDateTime(dateTime);

        return timeOnly;
    }
}