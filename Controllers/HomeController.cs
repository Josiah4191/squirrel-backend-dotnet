// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SquirrelTracker.Models;

namespace SquirrelTracker.Controllers
{
    // Handles the main home page of the app
    public class HomeController : Controller
    {
        // Used for logging information and errors
        private readonly ILogger<HomeController> _logger;

        // Sets up the logger when the controller starts
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Shows the home page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Shows the not found page
        [HttpGet]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        // Shows an error page if something goes wrong
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
