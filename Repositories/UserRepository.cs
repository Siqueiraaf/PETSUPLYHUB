using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;
    
    public UserRepository(UserManager<Users> userManager, SignInManager<Users> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<IdentityResult> CreateUserAsync(Users user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }
    
    public async Task<Users> FindUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user ?? throw new InvalidOperationException("Usuário não encontrado.");
    }
    
    public async Task<SignInResult> CheckPasswordSignInAsync(Users user, string password)
    {
        return await _signInManager.CheckPasswordSignInAsync(user, password, false);
    }
    
    public async Task<IList<string>> GetUserRolesAsync(Users user)
    {
        return await _userManager.GetRolesAsync(user);
    }
    
    public async Task AddUserToRoleAsync(Users user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<Users?> FindUserByIdAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<string> GeneratePasswordResetTokenAsync(Users user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(Users user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<IdentityResult> UpdateUserAsync(Users user)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteUserAsync(Users user)
    {
        return await _userManager.DeleteAsync(user);
    }
}

