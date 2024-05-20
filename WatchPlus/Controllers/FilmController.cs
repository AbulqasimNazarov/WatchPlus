#pragma warning disable CS1998
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;

namespace WatchPlus.Controllers;

// {controller=Home}/{action=Index}/{id?}
public class FilmController : Controller
{
    private readonly IFilmService filmService;
    private readonly IFilmRepository filmRepository;

    public FilmController(
        IFilmService filmService,
        IFilmRepository filmRepository)
    {

        this.filmRepository = filmRepository;
        this.filmService = filmService;
    }

    [HttpGet]
    [Route("[controller]/{film.Name}/{id}")]
    public async Task<IActionResult> InfoAboutFilmAsync(int id)
    {
        var film = await filmService.GetFilmAsync(id);

        return View(film);

    }

    [HttpGet("[controller]")]
    // /Film/Index
    public async Task<IActionResult> Index()
    {


        return View();
    }

    [HttpPost]
    [Route("[controller]")]
    // POST: /film
    public async Task<IActionResult> AddNewFilm(Film newFilm)
    {
        await filmService.CreateNewFilmAsync(newFilm);


        return base.RedirectToAction(actionName: "Index");
    }


}
