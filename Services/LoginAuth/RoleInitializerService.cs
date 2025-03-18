
using Microsoft.AspNetCore.Identity;

namespace Backend.Services.LoginAuth;
public class RoleInitializerService : IRoleInitializerService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleInitializerService(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task EnsureRolesCreatedAsync()
    {
        string[] roles = { "Admin", "Cliente" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid> 
                { 
                    Id = Guid.NewGuid(), 
                    Name = role, 
                    NormalizedName = role.ToUpper() 
                });
            }
        }
    }
}
