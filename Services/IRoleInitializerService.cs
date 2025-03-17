namespace Backend.Services;

public interface IRoleInitializerService
{
    Task EnsureRolesCreatedAsync();
}
