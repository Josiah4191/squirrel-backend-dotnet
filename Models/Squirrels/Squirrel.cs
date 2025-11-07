// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Stashes;
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Squirrels
{
    public class Squirrel
    {
        [Required]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(40)]
        public string Name { get; set; } = null!;

        public List<Stash> StashList { get; set; } = new();
    }
}
