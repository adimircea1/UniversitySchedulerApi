using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class StudentAttendance : IEntity
{
    [Validate(1)] public int StudentId { get; set; }

    [Validate(1)] public int CourseId { get; set; }

    [Validate("01/01/0001")] public DateOnly DateOfTheCourse { get; set; }

    [Validate("00:00:00", "23:59:59.9999999")]
    public TimeOnly TimeOfTheCourse { get; set; }

    public Student? Student { get; set; }
    public Course? Course { get; set; }

    [Validate(1)] public int Id { get; set; }
}