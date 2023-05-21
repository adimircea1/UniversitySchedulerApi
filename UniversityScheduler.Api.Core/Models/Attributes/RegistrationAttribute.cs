using UniversityScheduler.Api.Core.Enums;

namespace UniversityScheduler.Api.Core.Models.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegistrationAttribute : Attribute
{
    public RegistrationKind Type { get; set; }
}