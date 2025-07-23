
using Microsoft.EntityFrameworkCore;
using TShop.Application.Interfaces.Repositories;
using TShop.Application.Interfaces.Sevices;
using TShop.Application.Mapping;
using TShop.Application.Services;
using TShop.Infrustructure.Data;
using TShop.Infrustructure.Repositories;
using Serilog;
using Serilog.Events;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()  // নিজের ইনফরমেশন লগ দেখাবে
    .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal) // Microsoft এর ডিবাগ/ইনফো লগ বন্ধ
    .MinimumLevel.Override("System", LogEventLevel.Fatal)    // System এর ডিবাগ/ইনফো লগ বন্ধ
    .WriteTo.File(
        "Logs/log-.txt",                        // লগ ফাইল লোকেশন
        rollingInterval: RollingInterval.Day,  // প্রতিদিন নতুন ফাইল তৈরি হবে
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Message:lj}{NewLine}" // লগ আউটপুট ফরম্যাট
    )
    .WriteTo.Console(                          // চাইলে কনসোলেও লগ দেখাবে
        outputTemplate: "[{Timestamp:HH:mm:ss}] {Message:lj}{NewLine}")
    .CreateLogger();

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
