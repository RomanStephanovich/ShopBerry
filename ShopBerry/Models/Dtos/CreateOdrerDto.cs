using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShopBerry.Models.Dtos
{
    public class CreateOdrerDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }


        public List<OrderItemDto> OrderItems { get; set; }
    }
}
