using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Contracts;
using Backend.Models;
using Backend.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;
public class UserAuthService(
    IUserRepository userRepository,
    IConfiguration configuration,
    IValidator<RegisterUserDto> registerValidator,
    IValidator<UpdateUserDto> updateUserValidator,
    IUpdateUserService updateUserService) : IUserAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUpdateUserService _updateUserService = updateUserService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IValidator<RegisterUserDto> _registerValidator = registerValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator = updateUserValidator;

    public async Task<IActionResult> RegisterUserAsync(RegisterUserDto model)
    {
        // Validation of the RegisterUserDto before creating the user
        var validationResult = await _registerValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors);
        }

        var user = new Users 
        { 
            UserName = model.Email, 
            Email = model.Email, 
            FullName = model.FullName 
        };

        var result = await _userRepository.CreateUserAsync(user, model.Password);
        if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);
        
        await _userRepository.AddUserToRoleAsync(user, model.Role);
        return new OkObjectResult(
            new { message = "Usuário registrado com sucesso!" });
    }

    public async Task<IActionResult> UpdateUserAsync(Guid userId, UpdateUserDto model)
    {
        var validationResult = await _updateUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors);
        }

        return await _updateUserService.UpdateUserAsync(userId, model);
    }

    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.FindUserByIdAsync(userId);
        if (user == null)
        {
            return new NotFoundObjectResult(
                new { Success = false, Message = "Usuário não encontrado" });
        }

        var result = await _userRepository.DeleteUserAsync(user);
        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(
                new { Success = false, Message = "Erro ao excluir o usuário", Errors = result.Errors });
        }

        return new OkObjectResult(new { Success = true, Message = "Usuário excluído com sucesso!" });
    }

    private async Task<string> GenerateJwtToken(Users user)
    {
        var jwtKey = _configuration["Jwt:Key"] 
            ?? throw new InvalidOperationException("JWT Key is not configured.");

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
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
