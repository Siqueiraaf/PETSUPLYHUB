namespace Backend.Services.LoginAuth;

public interface ILoginService
{
    Task<string?> AuthenticateUserAsync(string email, string password);
}
