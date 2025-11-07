// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Items.Dto;

namespace SquirrelTracker.Models.StashLines.Dto
{
    public class StashLineDto
    {
        public ItemDto Item { get; set; }

        public int Quantity { get; set; }

        public StashLineDto() { }

        public StashLineDto(int quantity,  ItemDto item)
        {
            this.Quantity = quantity;
            this.Item = item;
        }
    }
}
