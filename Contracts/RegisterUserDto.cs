namespace Backend.Contracts;

public record RegisterUserDto(
    string FullName,
    string Email,
    string Password,
    string Role
);