// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Items;
using SquirrelTracker.Models.Stashes;
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.StashLines
{
    public class StashLine
    {
        [Required]
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public int StashId { get; set; }

        public Stash Stash { get; set; } = null!;

        [Required]
        public int ItemId { get; set; }

        public Item Item { get; set; } = null!;


    }
}
