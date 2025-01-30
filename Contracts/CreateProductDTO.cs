using System.ComponentModel.DataAnnotations;
using Backend.Models;

namespace Backend.Contracts
{
    public class CreateProductDTO
    {
        /// <summary>
        /// Nome do produto.
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string Name { get; init; }
        
        /// <summary>
        /// Categoria do produto.
        /// </summary>
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [MinLength(3, ErrorMessage = "A categoria deve ter pelo menos 3 caracteres.")]
        public string Category { get; init; }
        
        /// <summary>
        /// Espécie animal relacionada ao produto.
        /// </summary>
        [Required(ErrorMessage = "A espécie do animal é obrigatória.")]
        [RegularExpression("^(Cachorro|Gato|Pássaro|Peixe|Outro)$", ErrorMessage = "Espécie inválida.")]
        public string AnimalSpecie { get; init; }
        
        /// <summary>
        /// Descrição do produto (opcional).
        /// </summary>
        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Description { get; set; } = "Descrição não fornecida.";
        
        /// <summary>
        /// Preço do produto.
        /// </summary>
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// Quantidade em estoque do produto.
        /// </summary>
        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
        public int StockQuantity { get; set; } 

        
        // Converte o DTO para o modelo de domínio Product.
        public Product ConvertToModel()
        {
            return new Product
            {
                Name = Name,
                Category = Category,
                AnimalSpecie = AnimalSpecie,
                Description = Description,
                Price = Price,
                StockQuantity = StockQuantity
            };
        }
    }
}
