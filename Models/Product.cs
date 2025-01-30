using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // Identificador primário no banco
        public Guid Uuid { get; set; } = Guid.NewGuid(); // UUID para consultas externas (somente leitura)
        
        public string Name { get; set; }
        public string Category { get; set; }
        public string AnimalSpecie { get; set; } 
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } 
        
        public Product()
        {
            Uuid = Guid.NewGuid();
        }
    }
}    
      
