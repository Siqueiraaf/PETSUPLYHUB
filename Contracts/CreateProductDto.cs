namespace Backend.Contracts
{
    public record CreateProductDto(
        Guid PublicId,
        string Name,
        string Description,
        string Category,
        string AnimalSpecie,
        decimal Price
    );
}