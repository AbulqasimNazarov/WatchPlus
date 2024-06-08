using System.Reflection;
using ConfigurationApp.Options.Connections;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WatchPlus.Data;
using WatchPlus.Middlewares;
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


var app = builder.Build();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
