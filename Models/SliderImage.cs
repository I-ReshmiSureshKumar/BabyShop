using System.ComponentModel.DataAnnotations;

namespace BabyShop.Models
{
    public class SliderImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public string? CaptionTitle { get; set; }

        public string? CaptionText { get; set; }
    }
}
