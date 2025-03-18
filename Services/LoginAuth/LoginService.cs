using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.LoginAuth;
public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<Users> _signInManager;
    private readonly IConfiguration _configuration;

    public LoginService(IUserRepository userRepository, SignInManager<Users> signInManager, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<string?> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.FindUserByEmailAsync(email);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user.UserName ?? string.Empty, password, false, false);
        if (!result.Succeeded) return null;

        return await GenerateJwtToken(user);
    }

    private async Task<string> GenerateJwtToken(Users user)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.FullName ?? string.Empty)
        };

        var userRoles = await _userRepository.GetUserRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
