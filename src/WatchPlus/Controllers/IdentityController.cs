using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Services.Base;
#pragma warning disable CS1998

namespace WatchPlus.Controllers;

public class IdentityController : Controller
{
    public IUserService userService;
    private readonly IDataProtector dataProtector;
    private readonly IValidator<User> userValidator;


    public IdentityController(IValidator<User> userValidator, IUserService userService, IDataProtectionProvider dataProtectionProvider)
    {
        this.userService = userService;
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
        this.userValidator = userValidator;
    }

    [Route("/[controller]/[action]", Name = "LoginView")]
    public IActionResult Login(string? ReturnUrl)
    {
        var errorMessage = base.TempData["error"];

        ViewBag.ReturnUrl = ReturnUrl;

        if (errorMessage != null)
        {
            base.ModelState.AddModelError("All", errorMessage.ToString()!);
        }


        return base.View();
    }

    [Route("api/[controller]/[action]", Name = "LoginEndPoint")]
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        var foundUser = await userService.GetUserByEmailAsync(loginDto.Email);
        if (foundUser == null || foundUser.Email != loginDto.Email || foundUser.Password != loginDto.Password)
        {
            base.TempData["error"] = "Incorrect login or password!";
            return base.RedirectToRoute("LoginView", new
            {
                loginDto.ReturnUrl
            });
        }

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, foundUser.Email),
        new Claim("Id", foundUser.Id.ToString()),
        new Claim(ClaimTypes.Name, foundUser.Name),
        new Claim("Image", foundUser.Image),
        new Claim("IsBlock", foundUser.IsBlocked.ToString()),
        new Claim(ClaimTypes.Role, foundUser.Role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        if (!string.IsNullOrWhiteSpace(loginDto.ReturnUrl))
        {
            return base.Redirect(loginDto.ReturnUrl);
        }

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
    }




    [Route("/[controller]/[action]", Name = "RegistrationView")]
    public IActionResult Registration()
    {
        if (TempData["error"] != null)
        {
            ModelState.AddModelError("All", "This Email already registered");
        }

        return base.View();
    }


    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
    public async Task<IActionResult> Registration([FromForm] RegistrationDto registrationDto, IFormFile image)
    {
        try
        {

            var user = new User
            {
                Name = registrationDto.Name,
                Surname = registrationDto.Surname,
                Email = registrationDto.Email,

                Password = registrationDto.Password,
            };

            var validationResult = userValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    System.Console.WriteLine(error.ErrorMessage);
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View();
            }


            await this.userService.CreateNewUserAsync(user, image);
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return base.RedirectToRoute("RegistrationView");
        }

        return base.RedirectToRoute("LoginView");
    }

    [HttpGet]
    [Route("/api/[controller]/[action]", Name = "LogOut")]
    public async Task<IActionResult> Logout(string? ReturnUrl)
    {

        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return base.RedirectToRoute("LoginView", new
        {
            ReturnUrl
        });
    }


    [HttpGet("[controller]/[action]/{id}")]
    public async Task<IActionResult> Image(Guid id)
    {
        var user = await userService.GetUserByIdAsync(id);
        var fileStream = System.IO.File.Open(user.Image!, FileMode.Open);
        return File(fileStream, "image/jpeg");
    }


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("[controller]/[action]", Name = "AdminManage")]
    public async Task<IActionResult> ManageRoles(string email)
    {
        if (email == null)
        {
            return base.View();
        }
        var user = await userService.GetUserByEmailAsync(email);
        return base.View(user);
    }

    [Authorize(Roles = "Moderator")]
    [HttpGet]
    [Route("[controller]/[action]", Name = "ModeratorManage")]
    public async Task<IActionResult> ModeratorManage(string email)
    {
        if (email == null)
        {
            return base.View();
        }
        var user = await userService.GetUserByEmailAsync(email);

        return base.View(user);
    }



    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost]
    [Route("[controller]/[action]", Name = "GiveAccess")]
    public async Task<IActionResult> GiveAccess([FromForm] string email, [FromForm] string role)
    {

        var foundUser = await userService.GetUserByEmailAsync(email);
        if (foundUser == null)
        {
            return NotFound();
        }

        foundUser.Role = role;
        await userService.UpdateUserRoleAsync(foundUser);


        return RedirectToAction("ManageRoles", new { email });

    }


    [Authorize(Roles = "Moderator")]
    [HttpPost]
    [Route("[controller]/[action]", Name = "IgnoreUser")]
    public async Task<IActionResult> IgnoreUser([FromForm] string email, [FromForm] string role)
    {

        var foundUser = await userService.GetUserByEmailAsync(email);
        if (foundUser == null)
        {
            return NotFound();
        }

        foundUser.IsBlocked = true;
        await userService.UpdateUserAsync(foundUser);


        return RedirectToRoute("ModeratorManage");

    }




    [Authorize]
    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> UserAccount()
    {
        return base.View();
    }


}
