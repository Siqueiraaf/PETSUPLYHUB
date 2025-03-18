using System.Net.Mime;
using Backend.Contracts.DTOs;
using Backend.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.User;

[Route("auth")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class UserController(IUserAuthService authService) : ControllerBase
{
    private readonly IUserAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto model)
    {
        return await _authService.RegisterUserAsync(model);
    }

    [HttpPut("update/{userId}")]
    public async Task<IActionResult> UpdateUserAuth(Guid userId, [FromBody] UpdateUserDto model)
    {
        var result = await _authService.UpdateUserAsync(userId, model);
    
        // Checa se é um BadRequest
        if (result is BadRequestObjectResult badRequest)
        {
            return BadRequest(badRequest.Value);  
        }
    
        // Checa se é um NotFound
        if (result is NotFoundObjectResult notFound)
        {
            return NotFound(notFound.Value);  
        }
    
        // Retorna a resposta de sucesso
        return Ok(new { message = "Usuário atualizado com sucesso!" });  
    }

    [HttpDelete("delete/{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        var result = await _authService.DeleteUserAsync(userId);
        // Verificando o tipo da resposta retornada
        if (result is BadRequestObjectResult badRequestResult)
        {
            var response = badRequestResult.Value as dynamic;
            if (response != null)
            {
                return BadRequest(new { Success = response.Success, Message = response.Message });
            }
            return BadRequest(new { Success = false, Message = "Erro desconhecido." });
        }
        if (result is NotFoundObjectResult notFoundResult)
        {
            var response = notFoundResult.Value as dynamic;
            if (response != null)
            {
                return NotFound(new { Success = response.Success, Message = response.Message });
            }
            return NotFound(new { Success = false, Message = "Usuário não encontrado" });
        }
        // Caso seja uma resposta de sucesso (OkObjectResult)
        if (result is OkObjectResult okResult)
        {
            var response = okResult.Value as dynamic;
            if (response != null)
            {
                return Ok(new { Success = response.Success, Message = response.Message });
            }
            return Ok(new { Success = true, Message = "Operação bem-sucedida." });
        }
        return StatusCode(500, "Erro inesperado.");
    }
}