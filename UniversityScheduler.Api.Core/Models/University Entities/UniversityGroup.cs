using System.ComponentModel.DataAnnotations.Schema;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public class UniversityGroup : IEntity
{
    [Validate(1)] public int CatalogueId { get; set; }

    [Validate(0)] public int NumberOfMembers { get; set; }

    [Validate(0)] public int MaxSize { get; set; }

    [Column(TypeName = "varchar(256)")] public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(256)")] public string DiscordLink { get; set; } = string.Empty;

    public UniversitySpecialisation Speciality { get; set; }
    public List<Student> Students { get; set; } = new();
    public Catalogue? Catalogue { get; set; }

    [Validate(1)] public int Id { get; set; }
}