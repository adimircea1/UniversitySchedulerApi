namespace UniversityScheduler.Api.Core.Models.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ValidateAttribute : Attribute
{
    public ValidateAttribute(object? minValue = null, object? maxValue = null)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    private object? MinValue { get; }
    private object? MaxValue { get; }

    public bool Validate(object propertyValue)
    {
        if (MaxValue is not null && MinValue is not null)
        {
            var propertyType = propertyValue.GetType();

            if (propertyType == typeof(int) &&
                ((int)propertyValue < (int)MinValue! || (int)propertyValue > (int)MaxValue!))
                return false;

            if (propertyType == typeof(string) &&
                (propertyValue.ToString()?.Length < (int)MinValue! ||
                 propertyValue.ToString()?.Length > (int)MaxValue!))
                return false;

            if (propertyType == typeof(DateOnly))
            {
                DateOnly.TryParse(propertyValue.ToString(), out var dateOnlyValue);

                if (dateOnlyValue < DateOnly.Parse(MinValue.ToString()!) ||
                    dateOnlyValue > DateOnly.Parse(MaxValue.ToString()!))
                    return false;
            }

            if (propertyType == typeof(TimeOnly))
            {
                TimeOnly.TryParse(propertyValue.ToString(), out var timeOnlyValue);

                if (timeOnlyValue < TimeOnly.Parse(MinValue.ToString()!) ||
                    timeOnlyValue > TimeOnly.Parse(MaxValue.ToString()!))
                    return false;
            }

            return true;
        }

        if (MaxValue is null && MinValue is not null)
        {
            var propertyType = propertyValue.GetType();

            if (propertyType == typeof(int) && (int)propertyValue < (int)MinValue!) return false;

            if (propertyType == typeof(string) && propertyValue.ToString()?.Length < (int)MinValue!) return false;

            if (propertyType == typeof(DateOnly))
            {
                DateOnly.TryParse(propertyValue.ToString(), out var dateOnlyValue);

                if (dateOnlyValue < DateOnly.Parse(MinValue.ToString()!)) return false;
            }

            if (propertyType == typeof(TimeOnly))
            {
                TimeOnly.TryParse(propertyValue.ToString(), out var timeOnlyValue);

                if (timeOnlyValue < TimeOnly.Parse(MinValue.ToString()!)) return false;
            }

            return true;
        }

        if (MinValue is null && MaxValue is not null)
        {
            var propertyType = propertyValue.GetType();

            if (propertyType == typeof(int) && (int)propertyValue > (int)MaxValue!) return false;

            if (propertyType == typeof(string) && propertyValue.ToString()?.Length > (int)MaxValue!) return false;

            if (propertyType == typeof(DateOnly))
            {
                DateOnly.TryParse(propertyValue.ToString(), out var dateOnlyValue);

                if (dateOnlyValue > DateOnly.Parse(MaxValue.ToString()!)) return false;
            }

            if (propertyType == typeof(TimeOnly))
            {
                TimeOnly.TryParse(propertyValue.ToString(), out var timeOnlyValue);

                if (timeOnlyValue > TimeOnly.Parse(MaxValue.ToString()!)) return false;
            }

            return true;
        }

        throw new InvalidDataException("Both MinValue and MaxValue are null");
    }
}