using System.Net.Mime;
using Backend.Contracts.DTOs;
using Backend.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.User;

[Route("auth")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserAuthService _authService;

    public UserController(IUserAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto model)
    {
        return await _authService.RegisterUserAsync(model);
    }

    /// <summary>
    /// Atualiza informações do usuário.
    /// </summary>
    [HttpPut("update/{userId}")]
    public async Task<IActionResult> UpdateUserAuth(Guid userId, [FromBody] UpdateUserDto model)
    {
        var result = await _authService.UpdateUserAsync(userId, model);
        return ConvertActionResult(result, "Usuário atualizado com sucesso!");
    }

    /// <summary>
    /// Exclui um usuário.
    /// </summary>
    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _authService.DeleteUserAsync(userId);
        return ConvertActionResult(result, "Usuário excluído com sucesso!");
    }

    /// <summary>
    /// Método auxiliar para padronizar retornos.
    /// </summary>
    private IActionResult ConvertActionResult(IActionResult result, string successMessage)
    {
        return result switch
        {
            BadRequestObjectResult badRequest => BadRequest(new { Success = false, Message = ExtractMessage(badRequest.Value) }),
            NotFoundObjectResult notFound => NotFound(new { Success = false, Message = ExtractMessage(notFound.Value) }),
            OkObjectResult ok => Ok(new { Success = true, Message = ExtractMessage(ok.Value, successMessage) }),
            _ => StatusCode(500, new { Success = false, Message = "Erro inesperado." })
        };
    }

    /// <summary>
    /// Extrai mensagens de erro/sucesso de um objeto dinâmico.
    /// </summary>
    private static string ExtractMessage(object? result, string defaultMessage = "Operação bem-sucedida.")
    {
        if (result is { } obj && obj.GetType().GetProperty("Message") != null)
        {
            return obj.GetType().GetProperty("Message")?.GetValue(obj)?.ToString() ?? defaultMessage;
        }
        return defaultMessage;
    }
}
