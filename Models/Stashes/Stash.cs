// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Squirrels;
using SquirrelTracker.Models.StashLines;
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Stashes
{
    public class Stash

    {
        public int Id { get; set; }

        [Required, MinLength(5), MaxLength(255)]
        public string Location { get; set; } = null!;

        public int SquirrelId { get; set; }

        public Squirrel Squirrel { get; set; } = null!;

        public List<StashLine> StashLines { get; set; } = null!;
    }

}