using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShopBerry.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        
        public Product? Products { get; set; }
    }
}
