using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Models;

namespace WatchPlus.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpGet]

    public IActionResult Index()
    {
        var repoJson = new JsonRepository();

        var films = repoJson.GetAll("./Files/films.json");

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
