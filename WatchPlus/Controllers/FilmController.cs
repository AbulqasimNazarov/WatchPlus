#pragma warning disable CS1998
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Models;

namespace WatchPlus.Controllers;

// {controller=Home}/{action=Index}/{id?}
public class FilmController : Controller
{

    [HttpGet]
    [Route("[controller]/{film.Name}/{id}")]
    public async Task<IActionResult> InfoAboutFilmAsync(int id)
    {
        var repoJson = new JsonRepository();

        var films = repoJson.GetAll("./Files/films.json");
        if (films != null)
        {
            var film = films.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                return View(film);

            }
        }
        return NotFound("Film not found");

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

        var repoJson = new JsonRepository();

        repoJson.CreateFilm(newFilm);

        return base.RedirectToAction(actionName: "Index");
    }


}
