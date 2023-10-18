using System.Security.Claims;
using DataAccess.Models;

namespace StudentHub.Core.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// Generates JWT Token
    /// </summary>
    /// <param name="user">user model to generate JWT token</param>
    /// <returns>Returns token</returns>
    Task<string> CreateTokenAsync(User user);
}
