namespace Backend.Contracts
{
    public record CreateProductDto(
        string Name,
        string AnimalSpecie,
        string Category,
        string Description,
        decimal Price
    );
}