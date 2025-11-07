// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Stashes.Dto
{
    public class StashCreateDto
    {
        [Required(ErrorMessage = "Squirrel ID is required.")]
        public int SquirrelId { get; set; }

        [Required(ErrorMessage = "Location is required."), MinLength(5), MaxLength(255)]
        public string Location { get; set; } = null!;

        public StashCreateDto() { }

        public StashCreateDto(int squirrelId, string location)
        {
            SquirrelId = squirrelId;
            Location = location;
        }
    }
}
