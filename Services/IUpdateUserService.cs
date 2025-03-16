using Backend.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public interface IUpdateUserService
{
    Task<IActionResult> UpdateUserAsync(Guid userId, UpdateUserDto model);
}

