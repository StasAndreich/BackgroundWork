using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackgroundWork.Models;
using BackgroundWork.BackgroundService;

namespace BackgroundWork.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IBackgroundQueue _backgroundQueue;

    public HomeController(ILogger<HomeController> logger, IBackgroundQueue backgroundQueue)
    {
        _logger = logger;
        _backgroundQueue = backgroundQueue;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Imagine it is important endpoint for saving route data.
    [HttpPost]
    public async Task<IActionResult> SaveActivityData()
    {
        // Do saving simple plain data here.

        _backgroundQueue.QueueTask(async token =>
        {
            // Do time-consuming creation of Route preview.
            // Call methods with await, etc.
            Thread.Sleep(TimeSpan.FromSeconds(5));
        });

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

