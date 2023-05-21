using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class Catalogue : IEntity
{
    [Validate(1)] public int? UniversityGroupId { get; set; }

    public List<Grade> Grades { get; set; } = new();
    public UniversityGroup? UniversityGroup { get; set; }

    [Validate(1)] public int Id { get; set; }
}