using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class Grade : IEntity
{
    [Validate(1, 10)] public int Value { get; set; }

    [Validate(1)] public int StudentId { get; set; }

    [Validate(1)] public int CourseId { get; set; }

    [Validate(1)] public int CatalogueId { get; set; }

    public Catalogue? Catalogue { get; set; }

    [Validate(1)] public int Id { get; set; }
}