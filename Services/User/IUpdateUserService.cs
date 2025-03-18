using Backend.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.User;

public interface IUpdateUserService
{
    Task<IActionResult> UpdateUserAsync(Guid userId, UpdateUserDto model);
}

