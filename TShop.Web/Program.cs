
using Microsoft.EntityFrameworkCore;
using TShop.Application.Interfaces.Repositories;
using TShop.Application.Interfaces.Sevices;
using TShop.Application.Mapping;
using TShop.Application.Services;
using TShop.Infrustructure.Data;
using TShop.Infrustructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()  // নিজের লোগগুলোর জন্য
    .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal) // Microsoft এর সব কম গুরুত্বপূর্ণ লগ বন্ধ
    .MinimumLevel.Override("System", LogEventLevel.Fatal)    // System এর সব কম গুরুত্বপূর্ণ লগ বন্ধ
    .WriteTo.File("Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Message:lj}{NewLine}"
    )
    .CreateLogger();

builder.Host.UseSerilog();



builder.Host.UseSerilog();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Brand}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });
app.Run();


app.Run();
