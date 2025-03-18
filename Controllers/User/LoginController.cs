using Backend.Contracts;
using Backend.Contracts.DTOs;
using Backend.Services;
using Backend.Services.LoginAuth;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Backend.Controllers.User;

[Route("login")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class LoginController(ILoginService loginService) : ControllerBase
{
    private readonly ILoginService _loginService = loginService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserDto model)
    {
        var token = await _loginService.AuthenticateUserAsync(model.Email, model.Password);
        if (token == null) return Unauthorized(new { Message = "Credenciais inválidas" });

        return Ok(new { Token = token });
    }
}
