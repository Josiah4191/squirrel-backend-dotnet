// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Items.Dto;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Squirrels.Dto;
using SquirrelTracker.Models.Stashes.Dto;
using SquirrelTracker.Models.StashLines;
using SquirrelTracker.Models.StashLines.Dto;

namespace SquirrelTracker.Models.Stashes
{
    // Handles stash features and simple app logic
    public class StashService
    {
        // Repositories used to read/write data
        private readonly StashRepository _stashRepo;
        private readonly SquirrelRepository _squirrelRepo;
        private readonly StashLineRepository _stashLineRepo;
        private readonly ItemRepository _itemRepo;

        // Set up all needed repositories
        public StashService(StashRepository stashRepo, SquirrelRepository squirrelRepo, StashLineRepository stashLineRepo, ItemRepository itemRepo)
        {
            _stashRepo = stashRepo;
            _squirrelRepo = squirrelRepo;
            _stashLineRepo = stashLineRepo;
            _itemRepo = itemRepo;
        }

        // Get every stash and convert to simple data objects
        public List<StashDto> GetAllStashes() => _stashRepo
            .GetAllStashes()
            .Select(s => new StashDto(s.Id, s.Location, new SquirrelDto(s.Squirrel.Id, s.Squirrel.Name)))
            .ToList();

        // Get one stash by id and convert it
        public StashDto? GetStashById(int id)
        {
            Stash? stash = _stashRepo.GetStashById(id);
            if (stash == null) return null;

            return new StashDto(
                stash.Id,
                stash.Location,
                new SquirrelDto(stash.Squirrel.Id, stash.Squirrel.Name));
        }

        // Create a new stash for a squirrel
        public StashDto? CreateStash(StashCreateDto createDto)
        {
            // Find the squirrel first
            Squirrel? squirrel = _squirrelRepo.GetSquirrelById(createDto.SquirrelId);
            if (squirrel == null) return null;

            // Build the stash
            Stash stash = new Stash();
            stash.Squirrel = squirrel;
            stash.Location = createDto.Location;

            // Save it
            Stash newStash = _stashRepo.CreateStash(stash);

            // Return a simple result
            return new StashDto(
                newStash.Id,
                newStash.Location,
                new SquirrelDto(squirrel.Id, squirrel.Name));
        }

        // Change the location of a stash
        public StashDto? UpdateStashLocation(StashUpdateDto updateDto)
        {
            // Look up the stash
            Stash? stash = _stashRepo.GetStashById(updateDto.Id);
            if (stash == null) return null;

            // Save the new location
            Stash updatedStash = _stashRepo.UpdateStashLocation(stash, updateDto.Location);

            // Return a simple result
            return new StashDto(
                updatedStash.Id,
                updatedStash.Location,
                new SquirrelDto(
                    updatedStash.Squirrel.Id,
                    updatedStash.Squirrel.Name));
        }

        // Delete a stash
        public bool DeleteStash(int id)
        {
            // Look up the stash
            Stash? stash = _stashRepo.GetStashById(id);
            if (stash == null) return false;

            // Remove it
            _stashRepo.DeleteStash(stash);
            return true;
        }

        // Add an item to a stash
        public StashLineDto? AddItemToStash(StashLineSaveDto addDto)
        {
            // Look up needed records
            Stash? stash = _stashRepo.GetStashById(addDto.StashId);
            Item? item = _itemRepo.GetItemById(addDto.ItemId);
            if (stash == null || item == null) return null;

            // Build the stash line
            StashLine stashLine = new StashLine();
            stashLine.StashId = stash.Id;
            stashLine.ItemId = item.Id;
            stashLine.Quantity = addDto.Quantity;

            // Save it
            StashLine newStashLine = _stashLineRepo.CreateStashLine(stashLine);

            // Return a simple result
            return new StashLineDto(
                newStashLine.Quantity,
                new ItemDto(
                    newStashLine.Item.Id,
                    newStashLine.Item.Name,
                    newStashLine.Item.Description));
        }

        // Remove an item from a stash
        public bool DeleteItemFromStash(StashLineDeleteDto deleteDto)
        {
            // Find the stash line to delete
            StashLine? stashLine = _stashLineRepo.GetStashLineByStashIdAndItemId(deleteDto.StashId, deleteDto.ItemId);
            if (stashLine == null) return false;

            // Delete it
            _stashLineRepo.DeleteStashLine(stashLine);
            return true;
        }

        // Update how many of an item are in a stash
        public StashLineDto? UpdateItemQuantity(StashLineSaveDto updateDto)
        {
            // Find the stash line to change
            StashLine? stashLine = _stashLineRepo.GetStashLineByStashIdAndItemId(updateDto.StashId, updateDto.ItemId);
            if (stashLine == null) return null;

            // Save the new quantity
            StashLine updatedStashLine = _stashLineRepo.UpdateStashLineQuantity(stashLine, updateDto.Quantity);

            // Return a simple result
            return new StashLineDto(
                updatedStashLine.Quantity,
                new ItemDto(
                    updatedStashLine.Item.Id,
                    updatedStashLine.Item.Name,
                    updatedStashLine.Item.Description));
        }

        // Get a single stash line and convert it
        public StashLineDto? GetStashLine(int stashId, int itemId)
        {
            StashLine? stashLine = _stashLineRepo.GetStashLineByStashIdAndItemId(stashId, itemId);
            if (stashLine == null) return null;

            return new StashLineDto(
                stashLine.Quantity,
                new ItemDto(
                    stashLine.Item.Id,
                    stashLine.Item.Name,
                    stashLine.Item.Description));
        }
    }
}
