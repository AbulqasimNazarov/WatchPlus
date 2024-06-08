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
    private readonly IFilmService filmsService;
    

    public HomeController(IFilmService filmsService)
    {
        this.filmsService = filmsService;
        
    }


    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var moviesEF = await filmsService.GetAllFilmsAsync();

       
        return View(moviesEF);
    }


    

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
