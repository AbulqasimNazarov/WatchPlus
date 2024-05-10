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
    public async Task<IActionResult> InfoAboutTvShowAsync(int id)
    {
        var tvShow = await tvShowService.GetTvShowAsync(id);

        return View(tvShow);

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
    public async Task<IActionResult> AddNewTvShow(TvSHow newTvShow)
    {
        await tvShowService.CreateNewTvShowAsync(newTvShow);


        return base.RedirectToAction(actionName: "Index");
    }


}
