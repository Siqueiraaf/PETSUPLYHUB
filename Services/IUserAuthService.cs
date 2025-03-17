using Backend.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public interface IUserAuthService
{
    Task<IActionResult> RegisterUserAsync(RegisterUserDto model);
    Task<IActionResult> UpdateUserAsync(Guid userId,UpdateUserDto model);
    Task<IActionResult> DeleteUserAsync(Guid userId);
}
