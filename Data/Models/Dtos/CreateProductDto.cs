using System.ComponentModel.DataAnnotations;
namespace ShopBerry.Models.Dtos

{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Color { get; set; } = string.Empty;

        [Required]
        public int ShopId { get; set; } 
    }
}
