using Backend.Contracts;
using Backend.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;
public class UpdateUserService : IUpdateUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;

    public UpdateUserService(IUserRepository userRepository, IValidator<UpdateUserDto> updateUserValidator)
    {
        _userRepository = userRepository;
        _updateUserValidator = updateUserValidator;
    }

    public async Task<IActionResult> UpdateUserAsync(Guid userId, UpdateUserDto model)
    {
        var validationResult = await _updateUserValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var user = await _userRepository.FindUserByIdAsync(userId);
        if (user == null)
        {
            return new NotFoundObjectResult("Usuário não encontrado");
        }

        if (!string.IsNullOrEmpty(model.FullName))
        {
            user.FullName = model.FullName;
        }
        if (!string.IsNullOrEmpty(model.Email))
        {
            user.Email = model.Email;
            user.UserName = model.Email;
        }
        if (!string.IsNullOrEmpty(model.Password))
        {
            var resetToken = await _userRepository.GeneratePasswordResetTokenAsync(user);
            var passwordChangeResult = await _userRepository.ResetPasswordAsync(user, resetToken, model.Password);
            if (!passwordChangeResult.Succeeded)
            {
                return new BadRequestObjectResult(passwordChangeResult.Errors.Select(e => e.Description));
            }
        }

        var updateResult = await _userRepository.UpdateUserAsync(user);
        if (!updateResult.Succeeded)
        {
            return new BadRequestObjectResult(updateResult.Errors.Select(e => e.Description));
        }

        return new OkObjectResult(new { message = "Usuário atualizado com sucesso!" });
    }
}


