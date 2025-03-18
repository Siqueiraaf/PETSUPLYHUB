namespace Backend.Contracts.DTOs;

public record UpdateProductDto(
    string? Name,
    string? Description,
    string? Category,
    string? AnimalSpecie,
    decimal? Price
);

