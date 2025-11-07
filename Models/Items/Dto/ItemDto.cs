// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using System.ComponentModel.DataAnnotations;

namespace SquirrelTracker.Models.Items.Dto
{
    public class ItemDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required."), MinLength(2), MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required."), MinLength(2), MaxLength(255)]
        public string Description { get; set; } = null!;

        public ItemDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
