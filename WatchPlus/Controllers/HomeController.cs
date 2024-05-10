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
    private readonly ITvShowService tvService;
    private readonly MsSqlConnectionOptions tvShowSettings;

    public HomeController(ITvShowService tvService, 
        IOptions<MsSqlConnectionOptions> tvShowSettingsOptions)
    {
        this.tvService = tvService;
        this.tvShowSettings = tvShowSettingsOptions.Value;
    }


    [HttpGet]

    public async Task<IActionResult> Index()
    {
        //var service = new TVShowService();

        var tv = await tvService.GetTvShowsAsync();

        return View(tv);
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
