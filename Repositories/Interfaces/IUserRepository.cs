using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories;

public interface IUserRepository
{
        Task<IdentityResult> CreateUserAsync(Users user, string password);
        Task<Users> FindUserByEmailAsync(string email);
        Task<SignInResult> CheckPasswordSignInAsync(Users user, string password);
        Task<IList<string>> GetUserRolesAsync(Users user);
        Task AddUserToRoleAsync(Users user, string role);
        Task<Users?> FindUserByIdAsync(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Users user);
        Task<IdentityResult> ResetPasswordAsync(Users user, string token, string newPassword);
        Task<IdentityResult> UpdateUserAsync(Users user);
        Task<IdentityResult> DeleteUserAsync(Users user);
}
