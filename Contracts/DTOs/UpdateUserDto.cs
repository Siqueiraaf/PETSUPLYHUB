namespace Backend.Contracts.DTOs;

public record UpdateUserDto(
    string? FullName,
    string? Email,
    string? Password
);