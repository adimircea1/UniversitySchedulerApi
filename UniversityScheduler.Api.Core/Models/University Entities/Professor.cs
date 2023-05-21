using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class Professor : IUniversityMember
{
    [JsonConverter(typeof(JsonStringEnumConverter))] //this attribute will help me get enum values from requests
    public ProfessorCourseSpeciality CourseSpeciality { get; set; }

    public List<Course> Courses { get; set; } = new();

    [Validate(1)] public int Id { get; set; }

    [Column(TypeName = "varchar(256)")] public string FirstName { get; set; } = string.Empty;

    [Column(TypeName = "varchar(256)")] public string LastName { get; set; } = string.Empty;

    [Validate("01/01/0001")] public DateOnly BirthdayDate { get; set; }

    [Column(TypeName = "varchar(256)")] public string Email { get; set; } = string.Empty;

    [Validate(null, 10)]
    [Column(TypeName = "varchar(256)")]
    public string PhoneNumber { get; set; } = string.Empty;
}