// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.AspNetCore.Mvc;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Squirrels.Dto;

namespace SquirrelTracker.Areas.Admin.Controllers
{
    // Declare controller belongs to Admin area
    [Area("Admin")]
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

        // Shows the page to add a new squirrel
        [HttpGet]
        public IActionResult Create() => View("Create", new SquirrelCreateDto());

        // Handles adding a new squirrel
        [HttpPost]
        public IActionResult Create(SquirrelCreateDto createDto)
        {
            // Check if the createDto properties are valid
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            // Create the squirrel
            SquirrelDto squirrel = _squirrelService.CreateSquirrel(createDto);

            // Check if squirrel is null
            if (squirrel == null)
            {
                // Log a warning if there was a failure to create squirrel
                _logger.LogWarning("[Admin][SquirrelController][POST Create]: failed to create squirrel with name {SquirrelName}.", createDto.Name);
                return NotFound();
            }

            return RedirectToAction("Index", "Squirrel", new {area = ""});
        }

        // Shows the page to edit a squirrel
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Get the squirrel
            SquirrelDto? squirrelDto = _squirrelService.GetSquirrelById(id);

            // Check if the squirrel is null
            if (squirrelDto == null)
            {
                // Log a warning if there was a failure to create squirrel
                _logger.LogWarning("[Admin][SquirrelController][GET Edit]: squirrel with ID {SquirrelId} not found.", id);
                return NotFound();
            }

            // Convert to SquirrelUpdateDto so validation works in the view
            SquirrelUpdateDto updateDto = new SquirrelUpdateDto(squirrelDto.Id, squirrelDto.Name);

            return View("Edit", updateDto);
        }

        // Saves changes to a squirrel's name
        [HttpPost]
        public IActionResult Edit(int id, SquirrelUpdateDto updateDto)
        {
            // Check validation before updating
            if (!ModelState.IsValid)
            {
                return View("Edit", updateDto);
            }

            // Set the id
            updateDto.Id = id;

            // Update squirrel name
            SquirrelDto? squirrel = _squirrelService.UpdateSquirrelName(updateDto);

            // Check if updated squirrel is null
            if (squirrel == null)
            {
                // Log a warning if the squirrel ID doesn't exist
                _logger.LogWarning("[Admin][SquirrelController][POST Edit]: squirrel with ID {SquirrelId} not found.", id);
                return NotFound();
            }

            return RedirectToAction("Index", "Squirrel", new {area = ""});
        }

        // Removes a squirrel
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Delete squirrel
            bool deleted = _squirrelService.DeleteSquirrel(id);

            // Check if squirrel was deleted
            if (!deleted)
            {
                // Log a warning if there was a failure to delete the squirrel
                _logger.LogWarning("[Admin][SquirrelController][POST Delete]: failed to delete squirrel with squirrel ID {SquirrelId}.", id);
                return NotFound();
            }

            return RedirectToAction("Index", "Squirrel", new {area = ""});
        }
    }
}
