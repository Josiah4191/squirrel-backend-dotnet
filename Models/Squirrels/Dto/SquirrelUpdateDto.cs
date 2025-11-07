// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Squirrels.Dto
{
    public class SquirrelUpdateDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required."), MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        public SquirrelUpdateDto() { }

        public SquirrelUpdateDto(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
