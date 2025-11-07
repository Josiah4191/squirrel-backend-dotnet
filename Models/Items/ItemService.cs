// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Items.Dto;
using SquirrelTracker.Models.StashLines;
using SquirrelTracker.Models.StashLines.Dto;

namespace SquirrelTracker.Models.Items
{
    // Handles item-related logic between controller and repository
    public class ItemService
    {
        // Gives access to database actions for items
        private readonly ItemRepository _itemRepo;
        private readonly StashLineRepository _stashLineRepo;


        // Sets up the repository connection
        public ItemService(ItemRepository itemRepo, StashLineRepository stashLineRepo)
        {
            _itemRepo = itemRepo;
            _stashLineRepo = stashLineRepo;
        }

        // Get all items and convert them to data objects
        public List<ItemDto> GetAllItems() => _itemRepo
            .GetAllItems()
            .Select(i => new ItemDto(i.Id, i.Name, i.Description))
            .ToList();

        // Get a single item by id
        public ItemDto? GetItemById(int id)
        {
            // Look up the item
            Item? item = _itemRepo.GetItemById(id);
            if (item == null) return null;

            // Return the item data
            return new ItemDto(item.Id, item.Name, item.Description);
        }

        // Get all items that are addable for a stash
        public List<ItemDto> GetAddableItemsForStash(int stashId)
        {
            var allItems = GetAllItems();
            var stashLines = GetAllItemsInStash(stashId);

            var inStashIds = new HashSet<int>(stashLines.Select(sl => sl.Item.Id));
            var addable = allItems.Where(i => !inStashIds.Contains(i.Id)).ToList();
            return addable;
        }

        // Get all items in a stash and convert each one
        public List<StashLineDto> GetAllItemsInStash(int stashId)
        {
            return _stashLineRepo.GetStashLinesByStashId(stashId)
                .Select(sl => new StashLineDto(
                    sl.Quantity,
                    new ItemDto(
                        sl.Item.Id,
                        sl.Item.Name,
                        sl.Item.Description)))
                .ToList();
        }


    }
}
