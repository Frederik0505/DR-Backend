using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MusicRecords.Controllers;

public sealed class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // The sample has one administrator. Credentials must come from configuration,
    // not from the browser. Replace with the project's real US3 login when merging.
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var username = _configuration["Auth:Username"];
        var password = _configuration["Auth:Password"];
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
            || !SecureEquals(username, request.Username)
            || !SecureEquals(password, request.Password))
        {
            return Unauthorized(new { message = "Forkert brugernavn eller adgangskode." });
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Administrator")
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            username,
            roles = new[] { "Administrator" },
            expiresAtUtc = expiration
        });
    }

    private static bool SecureEquals(string expected, string actual)
    {
        // Hash both inputs first so the constant-time operation has equal-length operands.
        var left = SHA256.HashData(Encoding.UTF8.GetBytes(expected));
        var right = SHA256.HashData(Encoding.UTF8.GetBytes(actual ?? string.Empty));
        return CryptographicOperations.FixedTimeEquals(left, right);
    }
}
