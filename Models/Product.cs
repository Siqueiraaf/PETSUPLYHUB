namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; } // Chave primaria
        public Guid PublicId { get; set; } = Guid.NewGuid(); // Chave publica
        public string Name { get; set; } 
        public string AnimalSpecie { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}