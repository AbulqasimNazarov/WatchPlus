using System.Diagnostics;
using System.Text.Json;
using ConfigurationApp.Options.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WatchPlus.Models;
using WatchPlus.Repositories;
using WatchPlus.Repositories.Base;
using WatchPlus.Services;
using WatchPlus.Services.Base;

namespace WatchPlus.Controllers;

public class HomeController : Controller
{
    private readonly IFilmService filmService;
    private readonly MsSqlConnectionOptions tvShowSettings;

    public HomeController(IFilmService filmService, 
        IOptions<MsSqlConnectionOptions> tvShowSettingsOptions)
    {
        this.filmService = filmService;
        this.tvShowSettings = tvShowSettingsOptions.Value;
    }


    [HttpGet]

    public async Task<IActionResult> Index()
    {
        //var service = new TVShowService();

        var repoJson = new FilmJsonFileRepository();

        var films = await repoJson.GetAllAsync();

        return View(films);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
