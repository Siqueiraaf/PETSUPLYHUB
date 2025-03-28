using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Repositories;

namespace Backend.Middleware.Authentication;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration, IUserRepository userRepository)
    {
        _next = next;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            await AttachUserToContextAsync(context, token);
        }

        await _next(context);
    }

    private async Task AttachUserToContextAsync(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                
                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    var user = await _userRepository.FindUserByIdAsync(userId);
                    if (user != null)
                    {
                        context.Items["User"] = user;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao anexar usuário ao contexto: {ex.Message}");
        }
    }
}