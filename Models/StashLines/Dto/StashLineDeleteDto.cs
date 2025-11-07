// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.StashLines.Dto
{
    public class StashLineDeleteDto
    {
        [Required(ErrorMessage = "Stash ID is required.")]
        public int StashId { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        public StashLineDeleteDto(int stashid, int itemId)
        {
            StashId = stashid;
            ItemId = itemId;
        }
    }
}
