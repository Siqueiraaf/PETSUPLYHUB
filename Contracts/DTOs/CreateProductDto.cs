namespace Backend.Contracts.DTOs;

public record CreateProductDto(
    string Name,
    string AnimalSpecie,
    string Category,
    string Description,
    decimal Price
);
