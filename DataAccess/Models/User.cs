using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

public enum UserRole
{
    Admin,
    Teacher,
    Student
}

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    [NotMapped] public string FullName => $"{FirstName} {LastName}";

    public string? PhotoId { get; set; }

    public bool NewOpportunities { get; set; }
    public bool GetDiploma { get; set; }
    public string? Skills { get; set; }
    public string? Aim { get; set; }
    public string? Occupation { get; set; }
    public string? WantedLevel { get; set; }
    public string? About { get; set; }
    public string? Location { get; set; }
    public string? Sex { get; set; }
    public DateTime? BirthDate { get; set; }

    public int? Day => BirthDate?.Day;
    public int? Month => BirthDate?.Month;
    public int? Year => BirthDate?.Year;

    public UserRole Role { get; set; }
}
