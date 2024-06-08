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
public class TvShowController : Controller
{
    private readonly ITvShowService tvShowService;
    

    public TvShowController(
        ITvShowService tvShowService)
    {

        
        this.tvShowService = tvShowService;
    }

    [HttpGet]
    [Route("[controller]/{tvShow.Name}/{id}")]
    public async Task<IActionResult> InfoAboutTvShowAsync(Guid id)
    {
        var tvShow = await tvShowService.GetTvShowAsync(id);

        return View(tvShow);

    }


    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(Guid id) {
        
        var tvShow = await tvShowService.GetTvShowAsync(id);
        var fileStream = System.IO.File.Open(tvShow.Image!, FileMode.Open);
        return base.File(fileStream, "image/jpeg");
    }


    [HttpPost]
    [Route("[controller]/{id}")]
    public IActionResult DeleteTvShow(Guid id)
    {
        tvShowService.DeleteTvShowById(id);

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
       
    }



    [HttpGet("[controller]")]
    // /TvShow/Index
    public async Task<IActionResult> AddNewTvShow()
    {
        return View();
    }

    [HttpPost]
    [Route("[controller]", Name ="AddTvShow")]
    // POST: /tvShow
    public async Task<IActionResult> AddNewTvShow([FromForm] TvSHow newTvShow, IFormFile image)
    {
        await tvShowService.CreateNewTvShowAsync(newTvShow, image);


        return base.RedirectToRoute("AllTvShows");
    }


    [HttpGet]
    [Route("[controller]/All", Name="AllTvShows")]
    public async Task<IActionResult> GetAllTvShowsAsync()
    {
        var moviesEF = await tvShowService.GetAllTvShowsAsync();

       
        return View(moviesEF);
    }


}
