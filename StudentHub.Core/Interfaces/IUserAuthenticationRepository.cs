using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using StudentHub.Core.DTOs;

namespace StudentHub.Core.Interfaces;

public interface IUserAuthenticationRepository
{
    Task<IdentityResult> RegisterAdminAsync(UserRegistrationDto userRegistration);

    Task<IdentityResult> RegisterTeacherAsync(UserRegistrationDto userRegistration);
    Task<IdentityResult> RegisterStudentAsync(UserRegistrationDto userRegistration);

    /// <summary>
    /// Validates user credentials for JWT token
    /// </summary>
    /// <param name="loginDto">user credentials email and password</param>
    /// <returns>Returns user if valid otherwise null</returns>
    Task<User?> ValidateUser(UserLoginDto loginDto);

    Task<User?> GetUserByEmail(string? email);

    /// <summary>
    /// Generate token to reset password in case user forgot it
    /// </summary>
    /// <returns>Token to confirm email and reset password</returns>
    Task<string> GenerateChangePasswordTokenAsync(User user);

    /// <summary>
    /// Resets user password
    /// </summary>
    /// <param name="user">user to reset password</param>
    /// <param name="token">token to reset password that was sent by email</param>
    /// <param name="newPassword">new password</param>
    /// <returns>Returns Identity Result which indicate whenever password reset was successful</returns>
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
}
