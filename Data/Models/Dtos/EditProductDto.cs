using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopBerry.Models.Dtos
{
    public class EditProductDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Color { get; set; } = string.Empty;

        [ForeignKey("Shop")]
        public int ShopId { get; set; }
    }
}

