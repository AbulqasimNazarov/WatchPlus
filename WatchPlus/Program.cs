using System.Reflection;
using System.Security.Claims;
using ConfigurationApp.Options.Connections;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WatchPlus.Data;
using WatchPlus.Middlewares;
using WatchPlus.Models;
using WatchPlus.Repositories;
using WatchPlus.Repositories.Base;
using WatchPlus.Services;
using WatchPlus.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var msSqlConnectionSection = builder.Configuration.GetSection("Connections:MsSqlDb");
builder.Services.Configure<MsSqlConnectionOptions>(msSqlConnectionSection);

builder.Services.AddDbContext<WatchPlusDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MsSqlServer");
    options.UseSqlServer(connectionString);
});


builder.Services.AddTransient<IFilmService, FilmService>();
builder.Services.AddTransient<IFilmRepository, FilmEfRepository>();
builder.Services.AddTransient<ITvShowService, TVShowService>();
builder.Services.AddTransient<ITVShowRepository, TvShowEfRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILogRepository, LogDapperRepository>();
builder.Services.AddScoped<IUserRepository, UserEfRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WatchPlusPolicyWithRoles", policyBuilder =>
    {
        policyBuilder.RequireRole("Admin", "Moderator");
    });


});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WatchPlusDbContext>();
    await InitializeAdminUser(context);
}

async Task InitializeAdminUser(WatchPlusDbContext context)
{
    if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
    {
        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Surname = "User",
            Email = "admin@gmail.com",
            Password = "12345678a",

            Role = "Admin",

        };
        var adminAvatar = "wwwroot/Assets/IMG/user3.png";
        var extension = new FileInfo(adminAvatar).Extension[1..];
        var targetPath = Path.Combine("Assets", "UsersImg", $"{adminUser.Id}.{extension}");
        adminUser.Image = targetPath;

        
        var targetDirectory = Path.GetDirectoryName(targetPath);
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

       
        using (var sourceStream = System.IO.File.OpenRead(adminAvatar))
        using (var targetStream = System.IO.File.Create(targetPath))
        {
            await sourceStream.CopyToAsync(targetStream);
        }

        await context.Users.AddAsync(adminUser);
        await context.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
