using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GlobalSettingsService _settings;

    public HomeController(ILogger<HomeController> logger, GlobalSettingsService settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public IActionResult Index()
    {
        var globalSettings = _settings.GetGlobalSettings();
        string userId = HttpContext.Session.GetString("UserId");

        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToAction("Index", "Login");
        }
        return View();
    }

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
