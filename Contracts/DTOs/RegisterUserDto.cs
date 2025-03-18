namespace Backend.Contracts.DTOs;

public record RegisterUserDto(
    string FullName,
    string Email,
    string Password,
    string Role
);