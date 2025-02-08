namespace Backend.Contracts
{
    public record UpdateProductDto(
        Guid PublicId,          // Identificador único do produto a ser atualizado
        string? Name,           // Nome do produto (opcional)
        string? Description,    // Descrição do produto (opcional)
        string? Category,       // Categoria do produto (opcional)
        string? AnimalSpecie,   // Espécie de animal (opcional)
        decimal? Price          // Preço do produto (opcional)
    );
}
