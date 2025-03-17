namespace Backend.Services;

public interface ILoginService
{
    Task<string?> AuthenticateUserAsync(
        string email, 
        string password);
}
