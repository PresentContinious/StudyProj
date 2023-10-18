using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;

namespace StudentHub.Core.Services;

public class UserAuthenticationRepository : IUserAuthenticationRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserAuthenticationRepository(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<User?> GetUserByEmail(string? name)
    {
        if (name is null)
            return null;

        return await _userManager.FindByEmailAsync(name);
    }

    public async Task<IdentityResult> RegisterTeacherAsync(UserRegistrationDto userRegistration)
    {
        var user = new Teacher
        {
            UserName = userRegistration.Email,
            Email = userRegistration.Email,
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            PhoneNumber = userRegistration.PhoneNumber,
            Role = UserRole.Teacher
        };

        var result = await _userManager.CreateAsync(user, userRegistration.Password);
        await AddToTeacherRoleAsync(user);
        return result;
    }

    public async Task<IdentityResult> RegisterStudentAsync(UserRegistrationDto userRegistration)
    {
        var user = new Student
        {
            UserName = userRegistration.Email,
            Email = userRegistration.Email,
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            PhoneNumber = userRegistration.PhoneNumber,
            Role = UserRole.Student
        };

        var result = await _userManager.CreateAsync(user, userRegistration.Password);
        await AddToStudentRoleAsync(user);
        return result;
    }


    public async Task<IdentityResult> RegisterAdminAsync(UserRegistrationDto userRegistration)
    {
        var user = new User
        {
            UserName = userRegistration.Email,
            Email = userRegistration.Email,
            FirstName = userRegistration.FirstName,
            LastName = userRegistration.LastName,
            EmailConfirmed = true,
            Role = UserRole.Admin
        };

        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

        var result = await _userManager.CreateAsync(user, userRegistration.Password);
        await _userManager.AddToRoleAsync(user, "Admin");

        return result;
    }

    public async Task<User?> ValidateUser(UserLoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return null;

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        return result ? user : null;
    }

    public async Task<string> GenerateChangePasswordTokenAsync(User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    private async Task AddToTeacherRoleAsync(User user)
    {
        if (!await _roleManager.RoleExistsAsync("Teacher"))
            await _roleManager.CreateAsync(new IdentityRole("Teacher"));

        await _userManager.AddToRoleAsync(user, "Teacher");
    }

    private async Task AddToStudentRoleAsync(User user)
    {
        if (!await _roleManager.RoleExistsAsync("Student"))
            await _roleManager.CreateAsync(new IdentityRole("Student"));

        await _userManager.AddToRoleAsync(user, "Student");
    }
}
