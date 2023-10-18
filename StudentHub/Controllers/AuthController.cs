using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;
using StudentHub.Core.WorkModel;

namespace StudentHub.Controllers;

/// <summary>
/// Controller for authentication
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : Controller
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="workModel"></param>
    /// <param name="tokenService"></param>
    /// <param name="emailService"></param>
    public AuthController(IUnitOfWork workModel, ITokenService tokenService, IEmailService emailService)
    {
        WorkModel = workModel;
        TokenService = tokenService;
        EmailService = emailService;
    }

    private IUnitOfWork WorkModel { get; }
    private ITokenService TokenService { get; }
    private IEmailService EmailService { get; }

    /// <summary>
    /// Sign in user
    /// </summary>
    /// <param name="userLogin"></param>
    /// <response code="200">Token</response>
    /// <response code="400">Invalid username or password</response>
    [HttpPost]
    [ActionName("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] UserLoginDto userLogin)
    {
        var user = await WorkModel.UserAuthentication.ValidateUser(userLogin);
        if (user is null)
            return BadRequest("Invalid username or password");

        return Ok(new { token = await TokenService.CreateTokenAsync(user) });
    }

    /// <summary>
    /// Sign up teacher or student or admin
    /// </summary>
    /// <param name="userRegistration"></param>
    /// <response code="200">Token</response>
    /// <response code="400">Errors from registration</response>
    [HttpPost]
    [ActionName("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] UserRegistrationDto userRegistration)
    {
        IdentityResult res;

        if (userRegistration.Email == "admin@email.com")
            res = await WorkModel.UserAuthentication.RegisterAdminAsync(userRegistration);
        else if (userRegistration.Teacher)
            res = await WorkModel.UserAuthentication.RegisterTeacherAsync(userRegistration);
        else
            res = await WorkModel.UserAuthentication.RegisterStudentAsync(userRegistration);

        if (!res.Succeeded) return BadRequest(res.Errors);

        var login = new UserLoginDto { Email = userRegistration.Email, Password = userRegistration.Password };
        return await SignIn(login);
    }

    /// <summary>
    /// Send email with reset password link
    /// </summary>
    /// <param name="email"></param>
    /// <response code="200">If user exists, email with reset password link was sent to your email</response>
    [HttpPost]
    [ActionName("reset-password-request")]
    public async Task<IActionResult> ResetPasswordRequest([FromBody] string email)
    {
        var user = await WorkModel.UserAuthentication.GetUserByEmail(email);

        if (user is null)
            return Ok("If user exists, email with reset password link was sent to your email");

        var token = await WorkModel.UserAuthentication.GenerateChangePasswordTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);
        await EmailService.SendEmailAsync(email, "Reset password",
            $"Reset password <a href=https://localhost:44431/reset-password?email={email}&token={encodedToken}>link</a>");

        return Ok("If user exists, email with reset password link was sent to your email");
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="resetPassword"></param>
    /// <response code="200">Password was changed</response>
    /// <response code="400">Errors from reset password</response>
    /// <response code="404">User not found</response>
    [HttpPost]
    [ActionName("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
    {
        var user = await WorkModel.UserAuthentication.GetUserByEmail(resetPassword.Email);
        if (user is null)
            return NotFound("User not found");

        var res = await WorkModel.UserAuthentication
            .ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

        if (!res.Succeeded)
            return BadRequest(res.Errors);

        return Ok("Password was changed");
    }
}
