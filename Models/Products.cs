namespace Backend.Models
{
    public class Products
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string AnimalSpecie { get; set; }
        public decimal Price { get; set; }
    }
}