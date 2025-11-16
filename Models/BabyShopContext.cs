using Microsoft.EntityFrameworkCore;

namespace BabyShop.Models
{
    public class BabyShopContext : DbContext
    {
        public BabyShopContext(DbContextOptions<BabyShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }

    }
}
