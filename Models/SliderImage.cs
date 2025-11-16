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
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class SliderImageViewModel
    {
        public SliderImage NewSlider { get; set; }
        public List<SliderImage> SliderImages { get; set; }
        public List<Category> Categories { get; set; }

    }
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }



}
