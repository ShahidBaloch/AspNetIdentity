using AspNetIdentity.Data;
using AspNetIdentity.Models;
using AspNetIdentity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// why this is generic? to custmize or extend
//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();//24hours

// Register ASP.NET Core Identity Services using AddIdentity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
        options =>
        {
            // Password settings
            options.Password.RequireDigit = true;               // Must include digits
            options.Password.RequiredLength = 8;                // Minimum length 8
            options.Password.RequireNonAlphanumeric = true;     // Must include special characters
            options.Password.RequireUppercase = true;           // Must include uppercase letters
            options.Password.RequireLowercase = true;           // Must include lowercase letters
            options.Password.RequiredUniqueChars = 4;           // At least 4 unique characters
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
//By default, ASP.NET Core Identity’s email confirmation token (and other security tokens)
//are valid for 1 day (24 hours).
// Set token valid for 30 minutes
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30);
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // This sets the path to the login page users are redirected to if unauthenticated
    options.LoginPath = "/Account/Login";  // Change if your login page is elsewhere
});
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
