// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SquirrelTracker.Models.Stashes
{
    // Handles database actions for stashes
    public class StashRepository
    {
        // Connects to the main database
        private readonly SquirrelDbContext _context;

        // Sets up the database connection
        public StashRepository(SquirrelDbContext context)
        {
            _context = context;
        }

        // Get all stashes in the database
        public List<Stash> GetAllStashes() => _context.Stashes.ToList();

        // Get a single stash by its ID
        public Stash? GetStashById(int id)
        {
            return _context.Stashes
                .Include(s => s.Squirrel)
                .FirstOrDefault(s => s.Id == id);
        }

        // Get all stashes that belong to a specific squirrel
        public List<Stash> GetAllStashesBySquirrelId(int squirrelId)
        {
            return _context.Stashes
                .Include(s => s.Squirrel)
                .Where(s => s.Squirrel.Id == squirrelId)
                .ToList();
        }

        // Add a new stash to the database
        public Stash CreateStash(Stash stash)
        {
            EntityEntry<Stash> newStash = _context.Stashes.Add(stash);
            _context.SaveChanges();
            return newStash.Entity;
        }

        // Update the stash's location
        public Stash UpdateStashLocation(Stash stash, string location)
        {
            stash.Location = location;
            _context.Stashes.Update(stash);
            _context.SaveChanges();
            return stash;
        }

        // Delete a stash from the database
        public bool DeleteStash(Stash stash)
        {
            if (stash == null) return false;

            _context.Stashes.Remove(stash);
            _context.SaveChanges();
            return true;
        }
    }
}
