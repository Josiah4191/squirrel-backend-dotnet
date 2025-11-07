// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
namespace SquirrelTracker.Models.Squirrels.Dto
{
    public class SquirrelDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public SquirrelDto(int id, string name) {  
            Id = id; 
            Name = name; 
        }


    }
}
