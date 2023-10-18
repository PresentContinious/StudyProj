using Microsoft.AspNetCore.Http;

namespace StudentHub.Core.DTOs;

public class UserDetails
{
    public string? FullName { get; set; }
    public bool? NewOpportunities { get; set; }
    public bool? GetDiploma { get; set; }
    public string? Skills { get; set; }
    public string? Aim { get; set; }
    public string? Occupation { get; set; }
    public string? WantedLevel { get; set; }
    public string? About { get; set; }
    public string? Location { get; set; }
    public string? Sex { get; set; }
    public string? PhoneNumber { get; set; }
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
}
