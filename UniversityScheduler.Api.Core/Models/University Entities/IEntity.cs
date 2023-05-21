using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Models.University_Entities;

public interface IEntity
{
    [Validate(0)] public int Id { get; set; }
}