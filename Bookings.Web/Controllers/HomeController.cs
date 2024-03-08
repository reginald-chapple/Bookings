using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookings.Web.Models;
using Bookings.Web.Data;
using Microsoft.AspNetCore.Identity;
using Bookings.Web.Data.Services;
using Bookings.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Web.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<HomeController> _logger;
    private readonly ISeederService _seederService;

    public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context, ISeederService seederService)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
        _seederService = seederService;
    }

    [Route("~/")]
    public async Task<IActionResult> Index()
    {
        // if (_signInManager.IsSignedIn(HttpContext.User))
        // {
        //     var user = await _userManager.GetUserAsync(HttpContext.User);

        //     if (user != null && await _userManager.IsInRoleAsync(user, "Administrator"))
        //     {
        //         return RedirectToAction(nameof(Administrators.Controllers.HomeController.Index), "Home", new { area = "Administrators" });
        //     }
        //     else if (user != null && await _userManager.IsInRoleAsync(user, "Member"))
        //     {
        //         return RedirectToAction(nameof(Members.Controllers.HomeController.Index), "Home", new { area = "Members", id = user.Id });
        //     }
        //     else
        //     {
        //         return View();
        //     }
        // }
        var causes = await _context.Causes.Include(c => c.Children).OrderBy(c => c.Name).ToListAsync();
        return View(causes);
    }

    [Route("/Editor")]
    public IActionResult Editor()
    {
        return View();
    }

    [Route("/Setup")]
    public async Task<IActionResult> Setup()
    {
        await _seederService.RunAllAsync();
        return RedirectToAction(nameof(Index));
    }

    [Route("/Privacy")]
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
