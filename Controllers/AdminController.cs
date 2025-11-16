using BabyShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;



namespace BabyShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly BabyShopContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(IConfiguration configuration, BabyShopContext context, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _context = context;
            _env = env;
        }


        // GET: Admin/AdminLogin
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        // POST: Admin/AdminLogin
        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            string connectionString = _configuration.GetConnectionString("BabyShopConnection");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM UserDetails WHERE UserName=@username AND Password=@password";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // login success
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            ViewBag.Error = "Invalid username or password";
                            return View();
                        }
                    }
                }
            }
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult UserRegistration()
        {
            return View();
        }

        public IActionResult AddProducts()
        {
            return PartialView();
        }

        public IActionResult ViewOrders()
        {
            return PartialView();
        }

        public IActionResult ManageUsers()
        {
            return PartialView();
        }

        public IActionResult Offers()
        {
            return PartialView();
        }

        public IActionResult AdminSettings()
        {
            return PartialView();
        }
        public IActionResult HomepageSettings()
        {
            return PartialView();
        }
        public IActionResult AddNewProducts()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            if (model.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string folderPath = Path.Combine("wwwroot/images", fileName);

                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                model.ImageUrl = "/images/" + fileName;
            }

            _context.Product.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }
        public IActionResult SliderImagesForm()
        {
            var vm = new SliderImageViewModel()
            {
                NewSlider = new SliderImage(),
                SliderImages = _context.SliderImages.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SliderImagesForm(SliderImageViewModel model, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Save file
                string folder = "wwwroot/uploads/sliders/";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                model.NewSlider.ImageUrl = "/uploads/sliders/" + fileName;
            }

            _context.SliderImages.Add(model.NewSlider);
            _context.SaveChanges();

            return RedirectToAction("SliderImagesForm");
        }

    }
}
