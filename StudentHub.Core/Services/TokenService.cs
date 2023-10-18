using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Core.Interfaces;

namespace StudentHub.Core.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenService(UserManager<User> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.Email!)
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}
