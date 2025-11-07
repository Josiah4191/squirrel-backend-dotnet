// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SquirrelTracker.Models.Items;

namespace SquirrelTracker.Models.StashLines
{
    // Handles database actions for stash lines
    public class StashLineRepository
    {
        // Connects to the main database
        private readonly SquirrelDbContext _context;

        // Sets up the database connection
        public StashLineRepository(SquirrelDbContext context)
        {
            _context = context;
        }

        // Get every stash line in the database
        public List<StashLine> GetAllStashLines() => _context.StashLines.ToList();

        // Get one stash line by its ID
        public StashLine? GetStashLineById(int id) => _context.StashLines.Find(id);

        // Get one stash line that matches both stash and item IDs
        public StashLine? GetStashLineByStashIdAndItemId(int stashId, int itemId)
        {
            return _context.StashLines
                .Include(sl => sl.Item)
                .FirstOrDefault(sl => sl.StashId == stashId && sl.ItemId == itemId);
        }

        // Get all stash lines for one stash
        public List<StashLine> GetStashLinesByStashId(int stashId) => _context.StashLines
            .Include(sl => sl.Item)
            .Include(sl => sl.Stash)
            .Where(sl => sl.StashId == stashId)
            .ToList();

        // Get every item stored in a specific stash
        public List<Item> GetItemsByStashId(int stashId) => _context.StashLines
                .Where(s1 => s1.StashId == stashId)
                .Select(s1 => s1.Item)
                .ToList();

        // Get every item owned by a specific squirrel
        public List<Item> GetItemsBySquirrelId(int squirrelId) => _context.StashLines
                .Where(s1 => s1.Stash.SquirrelId == squirrelId)
                .Select(s1 => s1.Item)
                .ToList();

        // Add a new stash line to the database
        public StashLine CreateStashLine(StashLine stashLine)
        {
            EntityEntry<StashLine> newStashLine = _context.StashLines.Add(stashLine);
            _context.SaveChanges();
            return newStashLine.Entity;
        }

        // Change the quantity of a stash line
        public StashLine UpdateStashLineQuantity(StashLine stashLine, int quantity)
        {
            stashLine.Quantity = quantity;
            EntityEntry<StashLine> updatedStashLine = _context.StashLines.Update(stashLine);
            _context.SaveChanges();
            return updatedStashLine.Entity;
        }

        // Remove a stash line from the database
        public void DeleteStashLine(StashLine stashLine)
        {
            _context.StashLines.Remove(stashLine);
            _context.SaveChanges();
        }
    }
}
