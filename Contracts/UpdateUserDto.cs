namespace Backend.Contracts;

public record UpdateUserDto(
    string? FullName,
    string? Email,
    string? Password
);