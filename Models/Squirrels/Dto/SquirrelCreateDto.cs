// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Squirrels.Dto
{
    public class SquirrelCreateDto
    {

        [Required(ErrorMessage = "Name is required."), MinLength(2), MaxLength(40)]
        public string Name { get; set; } = null!;

    }
}
