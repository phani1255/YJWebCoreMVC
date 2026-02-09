using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using YJWebCoreMVC.Models;
using YJWebCoreMVC.Services;

namespace YJWebCoreMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GlobalSettingsService _settings;
    private readonly HelperCommonService _helperCommon;

    public HomeController(ILogger<HomeController> logger, GlobalSettingsService settings, HelperCommonService helperCommon)
    {
        _logger = logger;
        _settings = settings;
        _helperCommon = helperCommon;
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

    public string GetMultiStylesImages(string styles)
    {
        var data = _helperCommon.GetMultiStylesImages(styles);
        return JsonConvert.SerializeObject(data);
    }
}
