using Backend.Contracts;
using Backend.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.User;

public interface IUserAuthService
{
    Task<IActionResult> RegisterUserAsync(RegisterUserDto model);
    Task<IActionResult> UpdateUserAsync(Guid userId,UpdateUserDto model);
    Task<IActionResult> DeleteUserAsync(Guid userId);
}
