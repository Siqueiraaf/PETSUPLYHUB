namespace Backend.Services.LoginAuth;

public interface IRoleInitializerService
{
    Task EnsureRolesCreatedAsync();
}
