// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.AspNetCore.Mvc;
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Stashes;
using SquirrelTracker.Models.Stashes.Dto;
using SquirrelTracker.Models.StashLines.Dto;

namespace SquirrelTracker.Controllers
{
    // Handles actions for stashes and their items
    public class StashController : Controller
    {
        // Add logger
        private readonly ILogger<StashController> _logger;

        // Access to stash, squirrel, and item features
        private readonly StashService _stashService;
        private readonly ItemService _itemService;

        // Set up services for this controller
        public StashController(ILogger<StashController> logger, StashService stashService, ItemService itemService)
        {
            _stashService = stashService;
            _itemService = itemService;
            _logger = logger;
        }

        // Show all items inside a stash
        [HttpGet]
        public IActionResult Items(int id)
        {
            // Get items in this stash
            List<StashLineDto> stashLineDtos = _itemService.GetAllItemsInStash(id);

            // Keep info needed for links
            StashDto? stash = _stashService.GetStashById(id);

            // Check if stash is null
            if (stash == null)
            {
                // Log a warning if the stash ID doesn't exist before returning 404
                _logger.LogWarning("[StashController][GET Items]: stash with ID {StashId} not found.", id);
                return NotFound();
            }

            ViewBag.SquirrelId = stash.Squirrel.Id;
            ViewBag.StashId = id;

            // Show the items page
            return View("Items", stashLineDtos);
        }
    }
}