using Backend.Contracts.DTOs;
using Backend.Services.LoginAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers.User;

[Route("login")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IConfiguration _configuration;

    public LoginController(ILoginService loginService, IConfiguration configuration)
    {
        _loginService = loginService;
        _configuration = configuration;
    }

    /// <summary>
    /// Efetua o Login do usuário.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserDto model)
    {
        var user = await _loginService.AuthenticateUserAsync(model.Email, model.Password);
        if (user == null)
            return Unauthorized(new { Message = "Credenciais inválidas" });

        // Montagem do Token
        var keyString = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(keyString))
            throw new InvalidOperationException("A chave JWT não foi configurada corretamente.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.Role, "User"),
            new Claim("ler-dados-por-id", "true")
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}
