using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; internal set; }
        public int CategoryId { get; internal set; }
        public string CategoryName { get; internal set; }
        public int Stock { get; internal set; }
        public DateTime CreatedDate { get; internal set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; internal set; }
    }

}
