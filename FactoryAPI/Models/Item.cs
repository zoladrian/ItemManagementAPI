using System.ComponentModel.DataAnnotations;

namespace FactoryAPI.Models
{
    public class Item
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Range(0, 9999.99)]
        public decimal Price { get; set; }
    }
}