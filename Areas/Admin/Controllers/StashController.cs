// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.AspNetCore.Mvc;
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Items.Dto;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Stashes;
using SquirrelTracker.Models.Stashes.Dto;
using SquirrelTracker.Models.StashLines.Dto;

namespace SquirrelTracker.Areas.Admin.Controllers
{
    // Declare controller belongs to Admin area
    [Area("Admin")]
    public class StashController : Controller
    {
        // Add logger
        private readonly ILogger<StashController> _logger;
        // Access to stash, squirrel, and item features
        private readonly StashService _stashService;
        private readonly SquirrelService _squirrelService;
        private readonly ItemService _itemService;

        // Set up services for this controller
        public StashController(ILogger<StashController> logger, StashService stashService, SquirrelService squirrelService, ItemService itemService)
        {
            _stashService = stashService;
            _squirrelService = squirrelService;
            _itemService = itemService;
            _logger = logger;
        }

        // Show the page to add a new stash for a squirrel
        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.SquirrelId = id;

            // Show the create page
            return View("Create");
        }

        // Handle creating a new stash
        [HttpPost]
        public IActionResult Create(int id, StashCreateDto createDto)
        {
            createDto.SquirrelId = id;

            if (!ModelState.IsValid)
            {
                return View("Create", createDto);
            }

            // Make the stash
            StashDto? stashDto = _stashService.CreateStash(createDto);

            // Check if stashdto is null
            if (stashDto == null)
            {
                // Log a warning if there was a failure to create stash
                _logger.LogWarning("[Admin][StashController][POST Create]: failed to create stash with location {StashLocation}.", createDto.Location);
                return NotFound();
            }

            // Go back to that squirrel's stash list
            return RedirectToAction("Stashes", "Squirrel", new {area = "", id = stashDto.Squirrel.Id });
        }

        // Show the page to edit a stash
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Get the stash details
            StashDto? stashDto = _stashService.GetStashById(id);

            // Check if stash is null
            if (stashDto == null) 
            {
                // Log a warning if the stash ID doesn't exist before returning 404
                _logger.LogWarning("[Admin][StashController][GET Edit]: stash with stash ID {StashId} not found.", id);
                return NotFound();
            } 

            // Show the edit page
            return View("Edit", stashDto);
        }

        // Save changes to a stash
        [HttpPost]
        public IActionResult Edit(int id, StashUpdateDto updateDto)
        {

            // Make sure the right stash is updated
            updateDto.Id = id;

            // Check if dto is valid
            if (!ModelState.IsValid)
            {
                // Get the stash
                StashDto? stash = _stashService.GetStashById(id);

                // Check if the stash is null
                if (stash == null) 
                {
                    // Log a warning if the stash ID doesn't exist before returning 404
                    _logger.LogWarning("[Admin][StashController][POST Edit]: stash with stash ID {StashId} not found.", id);
                    return NotFound();
                }
                // Reload page and pass in stash dto
                return View("Edit", stash);
            }

            // Update the location
            StashDto? stashDto = _stashService.UpdateStashLocation(updateDto);

            if (stashDto == null)
            {
                // Log a warning if there was a failure to update the stash
                _logger.LogWarning("[Admin][StashController][POST Edit]: failed to update stash with location {StashLocation}", updateDto.Location);
                return NotFound();
            }

            // Go back to that squirrel's stash list
            return RedirectToAction("Stashes", "Squirrel", new {area = "", id = stashDto.Squirrel.Id });
        }

        // Remove a stash
        [HttpPost]
        public IActionResult Delete(int squirrelId, int stashId)
        {
            // Try to delete the stash
            bool removed = _stashService.DeleteStash(stashId);

            if (!removed)
            {
                // Log a warning if there was a failure to delete the stash
                _logger.LogWarning("[Admin][StashController][POST Delete]: failed to delete stash with stash ID {StashId}.", stashId);
                return NotFound();
            }

            // Return to the squirrel's stash list
            return RedirectToAction("Stashes", "Squirrel", new {area = "", id = squirrelId });
        }

        // Show the page to add an item to a stash
        [HttpGet]
        public IActionResult AddItem(int id)
        {
            // Get all items that can be added
            List<ItemDto> availableItems = _itemService.GetAddableItemsForStash(id);

            // Keep which stash we are adding to
            ViewBag.StashId = id;
            
            // Show the add item page
            return View("AddItem", availableItems);
        }

        // Add an item to the stash
        [HttpPost]
        public IActionResult AddItem(int stashId, int itemId, StashLineSaveDto addDto)
        {
            // add item id to the dto
            addDto.ItemId = itemId;

            // Make sure this is tied to the right stash
            addDto.StashId = stashId;

            // Add the item
            StashLineDto? stashLine = _stashService.AddItemToStash(addDto);

            // Check if stash line is null
            if (stashLine == null)
            {
                // Log a warning if there was a failure to add the stash line
                _logger.LogWarning("[Admin][StashController][POST AddItem]: failed to add stash line with stash ID {StashId} and item ID {ItemId}", stashId, itemId);
                return NotFound();
            }

            // Go back to the items list
            return RedirectToAction("Items", "Stash", new {area = "", id = addDto.StashId });
        }

        // Show the page to edit an item's quantity
        [HttpGet]
        public IActionResult EditItem(int stashId, int itemId)
        {
            // Get the stash line to edit
            StashLineDto? stashLine = _stashService.GetStashLine(stashId, itemId);

            // Check if stash line is null
            if (stashLine == null)
            {
                // Log a warning if the stash ID and item ID don't exist
                _logger.LogWarning("[Admin][StashController][GET EditItem]: stash line with stash ID {StashId} and item ID {ItemId} not found.", stashId, itemId);
                return NotFound();
            }

            // Keep stash id for links
            ViewBag.StashId = stashId;

            // Show the edit item page
            return View("EditItem", stashLine);
        }

        // Save the new quantity for an item
        [HttpPost]
        public IActionResult EditItem(int stashId, int itemId, StashLineSaveDto updateDto)
        {
            // Make sure the right stash and item are updated
            updateDto.StashId = stashId;
            updateDto.ItemId = itemId;

            // Update the quantity
            StashLineDto? stashLine = _stashService.UpdateItemQuantity(updateDto);

            // Check if stash line is null
            if (stashLine == null)
            {
                // Log a warning if there was a failure to update stash line quantity
                _logger.LogWarning("[Admin][StashController][POST EditItem]: failed to update stash line quantity with stash ID {StashId} and item ID {ItemId}.", stashId, itemId);
                return NotFound();
            }

            // Go back to the items list
            return RedirectToAction("Items", "Stash", new {area = "", id = stashId });
        }

        // Remove an item from the stash
        [HttpPost]
        public IActionResult DeleteItem(int stashId, int itemId)
        {
            // Try to remove the item
            var removed = _stashService.DeleteItemFromStash(new StashLineDeleteDto(stashId, itemId));

            // Check if the stash was removed
            if (!removed)
            {
                // Log a warning if there was a failure to delete the stash line
                _logger.LogWarning("[Admin][StashController][POST DeleteItem]: failed to delete stash line with stash ID {StashId} and item ID {ItemId}.", stashId, itemId);
                return NotFound();
            }

            // Go back to the items list
            return RedirectToAction("Items", "Stash", new {area = "", id = stashId });
        }


    }
}
