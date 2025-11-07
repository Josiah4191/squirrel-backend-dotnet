// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Squirrels.Dto;

namespace SquirrelTracker.Models.Stashes.Dto
{
    public class StashDto
    {
        public int Id { get; set; }

        public SquirrelDto Squirrel { get; set; } = null!;

        public string Location { get; set; } = null!;

        public StashDto(int id, string location, SquirrelDto squirrel)
        {
            Id = id;
            Squirrel = squirrel;
            Location = location;
        }
    }
}
