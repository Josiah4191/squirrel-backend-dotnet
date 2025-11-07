// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Stashes.Dto
{
    public class StashUpdateDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id {  get; set; }

        [Required(ErrorMessage = "Location is required."), MinLength(5), MaxLength(255)]
        public string Location { get; set; } = null!;

        public StashUpdateDto() { }

        public StashUpdateDto(int id, string location)
        {
            Id = id;
            Location = Location;
        }
    }
}
