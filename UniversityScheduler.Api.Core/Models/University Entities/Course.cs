using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class Course : IEntity
{
    [Validate(1)] public int ProfessorId { get; set; }

    public Professor? Professor { get; set; }

    [Validate(0)] public int NumberOfCredits { get; set; }

    [Column(TypeName = "varchar(256)")] public string CourseName { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CourseType Type { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProfessorCourseSpeciality CourseType { get; set; }

    public List<StudentAttendance> StudentAttendances { get; set; } = new();

    [Validate(1)] public int Id { get; set; }
}