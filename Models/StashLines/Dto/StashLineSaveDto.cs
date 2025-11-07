// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.StashLines.Dto
{
    public class StashLineSaveDto
    {
        [Required(ErrorMessage = "Stash ID is required.")]
        public int StashId { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } 

        public StashLineSaveDto() { }

        public StashLineSaveDto(int stashId, int itemId, int quantity)
        {
            StashId = stashId;
            ItemId = itemId;
            Quantity = quantity;
        }

    }
}
