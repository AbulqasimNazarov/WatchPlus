using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Services.Base;


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
    public async Task<IActionResult> AddNewFilm([FromForm] Film newFilm, IFormFile image)
    {
        var validationResult = filmValidator.Validate(newFilm);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                System.Console.WriteLine(error.ErrorMessage);
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return View();
        }
        await filmService.CreateNewFilmAsync(newFilm, image);
        return RedirectToRoute("AllFilms");
    }

    [HttpGet]
    [Route("[controller]/{film.Name}/{id}", Name = "FilmInfo")]
    public async Task<IActionResult> InfoAboutFilmAsync(Guid id)
    {
        var film = await filmService.GetFilmAsync(id);
        if (film == null)
        {
            return NotFound();
        }
        var comments = await filmService.GetFilmCommentsBYIdAsync(id);

        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst("Id");
        var userNameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);
        ViewBag.Comments = comments ?? new List<Comment>();
        if (userIdClaim != null)
        {
            var userIdString = userIdClaim.Value;
            var userNameString = userNameClaim.Value;

            if (Guid.TryParse(userIdString, out var userId))
            {
                var hasRated = await filmService.HasUserRatedFilmAsync(id, userId);
                ViewBag.HasRated = hasRated;
                ViewBag.ImagePath = userIdString;
                ViewBag.UserName = userNameString;

                //ViewBag.Comments = comments;

            }
        }
        else
        {
            ViewBag.HasRated = false;
        }
        return View(film);
    }

    [HttpPost]
    [Route("[controller]/{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteFilm(Guid id)
    {
        filmService.DeleteFilmById(id);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(Guid id)
    {
        var film = await filmService.GetFilmAsync(id);
        if (film == null || string.IsNullOrEmpty(film.Image))
        {
            return NotFound("Film or image not found.");
        }
        var fileStream = System.IO.File.Open(film.Image!, FileMode.Open);
        return File(fileStream, "image/jpeg");
    }

    [HttpGet("[controller]")]
    [Authorize(Roles = "Admin")]
    public IActionResult AddNewFilm()
    {
        return View();
    }

    [HttpGet]
    [Route("[controller]/All", Name = "AllFilms")]
    public async Task<IActionResult> GetAllFilmsAsync()
    {
        var moviesEF = await filmService.GetAllFilmsAsync();
        System.Console.WriteLine(moviesEF.GetType());
        return View(moviesEF);
    }

    [HttpGet]
    [Route("[controller]/found")]
    public async Task<IActionResult> GetFilmByName(string name)
    {
        var films = await filmService.GetAllFilmsbyNameAsync(name);
        return View(films);
    }

    [Authorize]
    [HttpPost]
    [Route("[controller]/RateFilm")]
    public async Task<IActionResult> RateFilm([FromBody] RatingRequest request)
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            var userIdString = userIdClaim.Value;

            if (Guid.TryParse(userIdString, out var userId))
            {
                if (await filmService.HasUserRatedFilmAsync(request.FilmId, userId))
                {
                    return BadRequest("You have already rated this film.");
                }

                await filmService.AddRatingAsync(request.FilmId, request.Rating, userId);

                var newRating = await filmService.GetAverageRatingAsync(request.FilmId);
                var roundedRating = Math.Round(newRating, 1);
                return Ok(new { newRating = roundedRating });
            }
            else
            {
                return BadRequest("Invalid user Id.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [Authorize]
    [HttpPost]
    [Route("[controller]/CommentFilm", Name = "AddComment")]
    public async Task<IActionResult> CommentFilm([FromBody] CommentRequestDto request)
    {
        try
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity?.FindFirst("Id");
            var userIdString = userIdClaim.Value;

            if (Guid.TryParse(userIdString, out var userId))
            {
                await filmService.AddCommentAsync(request.FilmId, request.Text, userId);
                return Ok();
            }
            else
            {
                return BadRequest("Invalid user Id.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPost]
    [Route("[controller]/[action]/{id}", Name = "DeleteComment")]
    [Authorize(Roles = "Moderator")]
    public IActionResult DeleteComment(Guid id)
    {
        try
        {
            filmService.DeleteComment(id);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }



}
