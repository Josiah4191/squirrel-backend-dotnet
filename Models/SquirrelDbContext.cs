// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SquirrelTracker.Controllers;
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.Stashes;
using SquirrelTracker.Models.StashLines;

namespace SquirrelTracker.Models
{
    // The main database context for the app
    public class SquirrelDbContext : DbContext
    {
        // Set up the database with provided options
        public SquirrelDbContext(DbContextOptions<SquirrelDbContext> options) : base(options) { }

        // Tables in the database
        public DbSet<Squirrel> Squirrels { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stash> Stashes { get; set; }
        public DbSet<StashLine> StashLines { get; set; }

        // Defines how tables relate and sets up starter data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<Squirrel> squirrelEntity = modelBuilder.Entity<Squirrel>();

            // A stash belongs to one squirrel, a squirrel can have many stashes
            modelBuilder.Entity<Stash>()
                .HasOne(s => s.Squirrel)
                .WithMany(q => q.StashList)
                .HasForeignKey(s => s.SquirrelId);

            // A stash line belongs to one stash, a stash can have many stash lines
            modelBuilder.Entity<StashLine>()
                .HasOne(sl => sl.Stash)
                .WithMany(s => s.StashLines)
                .HasForeignKey(sl => sl.StashId);

            // Prevents duplicate item entries in the same stash
            modelBuilder.Entity<StashLine>()
                .HasIndex(s1 => new { s1.StashId, s1.ItemId })
                .IsUnique();

            // A stash line is linked to one item
            modelBuilder.Entity<StashLine>()
                .HasOne(sl => sl.Item)
                .WithMany()
                .HasForeignKey(sl => sl.ItemId);

            // Adds some starter items into the database
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Acorn", Description = "A crunchy oak nut packed with energy — perfect for winter storage." },
                new Item { Id = 2, Name = "Pinecone", Description = "A scaly pine treasure filled with hidden seeds inside." },
                new Item { Id = 3, Name = "Walnut", Description = "A tough shell but worth the effort for the rich nut inside." },
                new Item { Id = 4, Name = "Maple Seed", Description = "A light, twirling seed that’s easy to carry and snack on." },
                new Item { Id = 5, Name = "Chestnut", Description = "Smooth and shiny — a favorite autumn treat for any squirrel." },
                new Item { Id = 6, Name = "Berry Mix", Description = "A colorful assortment of forest berries, sweet and juicy." },
                new Item { Id = 7, Name = "Mushroom Cap", Description = "Soft and savory, found under the shade of tall trees." },
                new Item { Id = 8, Name = "Hazelnut", Description = "A small round nut with a mild, buttery flavor." },
                new Item { Id = 9, Name = "Corn Kernel", Description = "Dried golden kernels, crunchy and filling for long trips." },
                new Item { Id = 10, Name = "Sunflower Seed", Description = "Tiny but full of flavor and fat — a top choice for energy." });
        }

        public static implicit operator SquirrelDbContext(SquirrelController v)
        {
            throw new NotImplementedException();
        }
    }
}
