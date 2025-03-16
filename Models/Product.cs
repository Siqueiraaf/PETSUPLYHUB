namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; } // Chave primaria
        public Guid PublicId { get; set; } = Guid.NewGuid(); // Chave publica
        public string Name { get; set; } = string.Empty;
        public string AnimalSpecie { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}