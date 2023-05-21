using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UniversityScheduler.Api.Infrastructure.DateOnlyAndTimeOnlySupport;

public class DateOnlyConverter : ValueConverter<DateOnly, string>
{
    public DateOnlyConverter() : base(date => date.ToString(),
        content => StringToDateOnly(content))
    {
    }

    private static DateOnly StringToDateOnly(string content)
    {
        DateTime.TryParse(content, out var dateTime);

        var dateOnly = DateOnly.FromDateTime(dateTime);

        return dateOnly;
    }
}