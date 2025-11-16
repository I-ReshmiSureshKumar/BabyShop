using BabyShop.Data;
using BabyShop.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BabyShopConnection");
builder.Services.AddDbContext<BabyShopContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("BabyShopConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StoredProcedureService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
