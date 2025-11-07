// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using SquirrelTracker.Models.Items.Dto;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Squirrels.Dto;
using SquirrelTracker.Models.Stashes.Dto;

namespace SquirrelTracker.Controllers
{
    // Handles everything related to squirrels
    public class SquirrelController : Controller
    {
        // Add logger
        private readonly ILogger<SquirrelController> _logger;
        // Gives access to squirrel data and methods
        private readonly SquirrelService _squirrelService;

        // Sets up the service when the controller starts
        public SquirrelController(ILogger<SquirrelController> logger, SquirrelService squirrelService)
        {
            _squirrelService = squirrelService;
            _logger = logger;
        }

        // Shows a list of all squirrels
        [HttpGet]
        public IActionResult Index()
        {
            List<SquirrelDto> squirrelDtos = _squirrelService.GetAllSquirrels();
            return View(squirrelDtos);
        }

        // Shows all stashes for a specific squirrel
        [HttpGet]
        public IActionResult Stashes(int id)
        {
            // Get all stashes and squirrel details
            List<StashDto> stashDtos = _squirrelService.GetAllStashes(id);
            SquirrelDto? squirrelDto = _squirrelService.GetSquirrelById(id);

            // Check if squirrel is null
            if (squirrelDto == null)
            {
                // Log a warning if the squirrel ID doesn't exist before returning 404
                _logger.LogWarning("[SquirrelController][GET Stashes]: squirrel with ID {SquirrelId} not found.", id);
                return NotFound();
            }

            // Store squirrel info for the view
            ViewBag.SquirrelName = squirrelDto.Name;
            ViewBag.SquirrelId = squirrelDto.Id;

            // Show stash list
            return View("Stashes", stashDtos);
        }

    }
}