using System.ComponentModel.DataAnnotations.Schema;
using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class Student : IUniversityMember
{
    [Validate(1)] public int? UniversityGroupId { get; set; }

    [Validate(1)] public int StudyYear { get; set; }

    [Validate(13)]
    [Column(TypeName = "varchar(13)")]
    public string Cnp { get; set; } = string.Empty;

    public UniversityGroup? UniversityGroup { get; set; }
    public List<StudentAttendance> StudentAttendances { get; set; } = new();

    [Validate(1)] public int Id { get; set; }

    [Column(TypeName = "varchar(256)")] public string FirstName { get; set; } = string.Empty;

    [Column(TypeName = "varchar(256)")] public string LastName { get; set; } = string.Empty;

    [Validate("01/01/0001")] public DateOnly BirthdayDate { get; set; }

    [Column(TypeName = "varchar(256)")] public string Email { get; set; } = string.Empty;

    [Validate(null, 10)]
    [Column(TypeName = "varchar(256)")]
    public string PhoneNumber { get; set; } = string.Empty;
}