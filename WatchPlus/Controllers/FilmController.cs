#pragma warning disable CS1998
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Data;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WatchPlus.Controllers;

// {controller=Home}/{action=Index}/{id?}
public class FilmController : Controller
{
    private readonly IFilmService filmService;
    
    private readonly IValidator<Film> filmValidator;
    public FilmController(IValidator<Film> filmValidator, IFilmService filmService)
    {
        this.filmService = filmService;
        this.filmValidator = filmValidator;
    }

    [HttpPost]
    [Route("[controller]", Name = "AddFilm")]
    // POST: /film
    public async Task<IActionResult> AddNewFilm([FromForm] Film newFilm, IFormFile image)
    {
        var validationResult = filmValidator.Validate(newFilm);

        if(validationResult.IsValid == false) {

            foreach (var error in validationResult.Errors)
            {
                System.Console.WriteLine(error.ErrorMessage);
                base.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return base.View();
        }

        await filmService.CreateNewFilmAsync(newFilm, image);
        
        return base.RedirectToRoute("AllFilms");
    }

    [HttpGet]
    [Route("[controller]/{film.Name}/{id}")]
    public async Task<IActionResult> InfoAboutFilmAsync(Guid id)
    {
        var film = await filmService.GetFilmAsync(id);

        return View(film);

    }


    [HttpPost]
    [Route("[controller]/{id}")]
    public IActionResult DeleteFilm(Guid id)
    {
        filmService.DeleteFilmById(id);

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
       
    }

    

    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(Guid id) {
        
        var film = await filmService.GetFilmAsync(id);
        var fileStream = System.IO.File.Open(film.Image!, FileMode.Open);
        return base.File(fileStream, "image/jpeg");
    }

    [HttpGet("[controller]")]
    // /Film/Index
    public async Task<IActionResult> AddNewFilm()
    {

        return View();
    }

    

    [HttpGet]
    [Route("[controller]/All", Name="AllFilms")]
    public async Task<IActionResult> GetAllFilmsAsync()
    {
        var moviesEF = await filmService.GetAllFilmsAsync();

       
        return View(moviesEF);
    }


}
