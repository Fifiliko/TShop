using Microsoft.EntityFrameworkCore;
using TShop.Infrustructure.Data;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });

app.Run();
