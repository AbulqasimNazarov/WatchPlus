using ConfigurationApp.Options.Connections;
using Microsoft.Extensions.Options;
using WatchPlus.Middlewares;

//using WatchPlus.Middlewares;
using WatchPlus.Repositories;
using WatchPlus.Repositories.Base;
using WatchPlus.Services;
using WatchPlus.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var msSqlConnectionSection = builder.Configuration.GetSection("Connections")
    .GetSection("MsSqlDb");

builder.Services.Configure<MsSqlConnectionOptions>(msSqlConnectionSection);




builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddTransient<IFilmRepository, FilmJsonFileRepository>();

builder.Services.AddScoped<ITvShowService, TVShowService>();
builder.Services.AddTransient<ITVShowRepository, TvShowDapperRepository>();

builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<ILogRepository, LogDapperRepository>();



builder.Services.AddScoped<ITvShowService>((serviceProvider) => {
    var serviceRepository = serviceProvider.GetRequiredService<ITVShowRepository>();

    return new TVShowService(serviceRepository);
});

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

app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

