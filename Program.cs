using Microsoft.EntityFrameworkCore;
using Beat2Book.Data;
using Beat2Book.Data;
using Beat2Book.Models;
using Microsoft.EntityFrameworkCore;
using Beat2Book.Repository;
using Beat2Book.Interfaces;
using Beat2Book.Helpers;
using Beat2Book.Services;
using Beat2Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using static Beat2Book.Hubs.ChatHub;
using Beat2Book.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBandRepository, BandRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IPhotoService, PhotoSevice>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();    

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings")); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSignalR(); 

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;

    //// Account lockout settings
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true; 

    // Custom CAPTCHA challenge after 3 failed login attempts
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); // Lockout duration for each failed attempt
    options.Lockout.MaxFailedAccessAttempts = 3; // CAPTCHA challenge triggered after 3 failed attempts
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


  

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(); 

var app = builder.Build(); 

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app); 
    // Seed.SeedData(app); 
}

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
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("Beat2Book");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("Beat2Book");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
