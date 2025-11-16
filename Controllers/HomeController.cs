using BabyShop.Data;
using BabyShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BabyShop.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly BabyShopContext _context;
        private readonly StoredProcedureService _sp;
        public HomeController(BabyShopContext context, StoredProcedureService sp)
        {
            _context = context;
            _sp = sp;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index(string? searchTerm)
        {
            // Pass NULL or empty string safely to the stored procedure
            var param = new Dictionary<string, object>
    {
        { "@searchItem", string.IsNullOrEmpty(searchTerm) ? null : searchTerm }
    };

            // Call SP
            DataTable dt = _sp.ExecuteStoredProcedure("SearchProducts", param);

            // Map the DataTable into Product list
            List<Product> products = MapProducts(dt);

            // Get slider images from EF Core (your original code)
            var sliders = _context.SliderImages.ToList();

            // Prepare VM
            var viewModel = new HomeViewModel
            {
                Products = products,
                Sliders = sliders
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            // Call stored procedure
            DataTable dt = _sp.ExecuteStoredProcedure("GetAllProducts");

            // Map DataTable to Product list
            List<Product> products = MapProducts(dt);

            // Find the product with the given id
            var product = products.FirstOrDefault(p => p.ProductId == id.Value);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminLogin()
        {
            return View("~/Views/Admin/AdminLogin.cshtml");

        }
        public IActionResult AdminSettings()
        {
            return View("~/Views/Admin/AdminSettings.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private List<Product> MapProducts(DataTable dt)
        {
            List<Product> list = new List<Product>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Product
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Name = SafeString(row["Name"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = SafeString(row["CategoryName"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    Description = SafeString(row["Description"]),
                    ImageUrl = SafeString(row["ImageUrl"]),
                    Stock = row["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(row["Stock"]),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    UpdatedDate = row["UpdatedDate"] == DBNull.Value ? null : Convert.ToDateTime(row["UpdatedDate"]),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                });
            }

            return list;
        }

        private string SafeString(object obj)
        {
            return obj == DBNull.Value ? "" : obj.ToString();
        }

    }
}
