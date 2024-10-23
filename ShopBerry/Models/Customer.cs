using System.ComponentModel.DataAnnotations;

namespace ShopBerry.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public ICollection<Order> Orders { get; set; }
    }
}
