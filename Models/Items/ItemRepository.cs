// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker

namespace SquirrelTracker.Models.Items
{
    // Handles database work for items
    public class ItemRepository
    {
        // Connects to the database
        private readonly SquirrelDbContext _context;

        // Sets up the database connection
        public ItemRepository(SquirrelDbContext context)
        {
            this._context = context;
        }

        // Get every item from the database
        public List<Item> GetAllItems() => _context.Items.ToList();

        // Get one item by id
        public Item? GetItemById(int id) => _context.Items.Find(id);
    }
}
