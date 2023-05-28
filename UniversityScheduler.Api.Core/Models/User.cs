using System.ComponentModel.DataAnnotations.Schema;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Models;

public class User : IEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = false;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Validate(1)]
    public int Id { get; set; }
}