using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallAdvertisements.Data.Context;
using SmallAdvertisements.Services;
using SmallAdvertisements.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AdvertisementsDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Db"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AdvertisementsDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<ILikeService, LikeService>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Users/Login";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();
