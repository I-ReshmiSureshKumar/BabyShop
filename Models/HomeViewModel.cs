namespace BabyShop.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<SliderImage> Sliders { get; set; } = new List<SliderImage>();
    }
}
