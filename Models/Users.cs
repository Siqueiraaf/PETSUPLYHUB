using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public class Users : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
}
