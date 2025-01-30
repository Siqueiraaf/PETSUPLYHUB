using System.ComponentModel.DataAnnotations;

namespace Backend.Contracts
{
    public class UpdateProductDTO
    {
        /// <summary>
        /// Nome do produto (opcional).
        /// </summary>
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string? Name { get; set; }
        
        /// <summary>
        /// Categoria do produto (opcional).
        /// </summary>
        [MinLength(3, ErrorMessage = "A categoria deve ter pelo menos 3 caracteres.")]
        public string? Category { get; set; }
        
        /// <summary>
        /// Espécie animal relacionada ao produto (opcional).
        /// </summary>
        [RegularExpression("^(Cachorro|Gato|Pássaro|Peixe|Outro)$", ErrorMessage = "Espécie inválida.")]
        public string? AnimalSpecie { get; set; }
        
        /// <summary>
        /// Descrição do produto (opcional).
        /// </summary>
        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string? Description { get; set; }
        
        /// <summary>
        /// Preço do produto (opcional).
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal? Price { get; set; }
        
        /// <summary>
        /// Quantidade em estoque do produto (opcional).
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque não pode ser negativa.")]
        public int? StockQuantity { get; set; }
    }
}
