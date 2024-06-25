using System.Diagnostics;
using System.Text.Json;
using ConfigurationApp.Options.Connections;
using Microsoft.AspNetCore.DataProtection;
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
    private readonly IUserService userService;
   


    public HomeController(IFilmService filmsService, IUserService userService)
    {
        this.filmsService = filmsService;
        this.userService = userService;
        
    }


    [HttpGet]
    //[Route("/[controller]/[action]", Name = "HomePage")]
    public async Task<IActionResult> Index()
    {
        var filmHighRate = await filmsService.GetFilmWithHighestRateAsync();
        

        return View(filmHighRate);
    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
