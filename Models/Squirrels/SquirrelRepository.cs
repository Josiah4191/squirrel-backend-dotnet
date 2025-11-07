// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SquirrelTracker.Models.Squirrels
{
    // Handles database work for squirrels
    public class SquirrelRepository
    {
        // Connection to the database
        private readonly SquirrelDbContext _context;

        // Set up the database connection
        public SquirrelRepository(SquirrelDbContext context)
        {
            _context = context;
        }

        // Get every squirrel
        public List<Squirrel> GetAllSquirrels() => _context.Squirrels.ToList();

        // Get one squirrel by id
        public Squirrel? GetSquirrelById(int id) => _context.Squirrels.Find(id);

        // Add a new squirrel
        public Squirrel CreateSquirrel(Squirrel squirrel)
        {
            EntityEntry<Squirrel> newSquirrel = _context.Squirrels.Add(squirrel);
            _context.SaveChanges();
            return newSquirrel.Entity;
        }

        // Change a squirrel's name
        public Squirrel UpdateSquirrelName(Squirrel squirrel, string name)
        {
            squirrel.Name = name;
            _context.Squirrels.Update(squirrel);
            _context.SaveChanges();
            return squirrel;
        }

        // Remove a squirrel
        public void DeleteSquirrel(Squirrel squirrel)
        {
            _context.Squirrels.Remove(squirrel);
            _context.SaveChanges();
        }
    }
}
