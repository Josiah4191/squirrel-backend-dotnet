// Josiah Stoltzfus, 11/03/2025, Squirrel Tracker
using SquirrelTracker.Models.Items.Dto;
using SquirrelTracker.Models.Squirrels.Dto;
using SquirrelTracker.Models.Stashes;
using SquirrelTracker.Models.Stashes.Dto;
using SquirrelTracker.Models.StashLines;

namespace SquirrelTracker.Models.Squirrels
{
    // Handles squirrel-related features and simple logic
    public class SquirrelService
    {
        // Repositories used to work with the database
        private readonly SquirrelRepository _squirrelRepo;
        private readonly StashLineRepository _stashLineRepo;
        private readonly StashRepository _stashRepo;

        // Set up needed repositories
        public SquirrelService(SquirrelRepository squirrelRepo, StashLineRepository stashLineRepo, StashRepository stashRepo)
        {
            _squirrelRepo = squirrelRepo;
            _stashLineRepo = stashLineRepo;
            _stashRepo = stashRepo;
        }

        // Get every squirrel and convert to simple data objects
        public List<SquirrelDto> GetAllSquirrels() => _squirrelRepo
            .GetAllSquirrels()
            .Select(s => new SquirrelDto(s.Id, s.Name))
            .Select(s => new SquirrelDto(s.Id, s.Name))
            .ToList();

        // Get one squirrel by id
        public SquirrelDto? GetSquirrelById(int id)
        {
            // Look up the squirrel
            Squirrel? squirrel = _squirrelRepo.GetSquirrelById(id);
            if (squirrel == null) return null;

            // Return a simple result
            return new SquirrelDto(squirrel.Id, squirrel.Name);
        }

        // Get all items owned by a squirrel
        public List<ItemDto> GetAllSquirrelItems(int id) => _stashLineRepo.GetItemsBySquirrelId(id)
                .Select(i => new ItemDto(i.Id, i.Name, i.Description))
                .ToList();

        // Create a new squirrel
        public SquirrelDto CreateSquirrel(SquirrelCreateDto createDto)
        {
            // Save the new squirrel
            Squirrel squirrel = _squirrelRepo.CreateSquirrel(new Squirrel { Name = createDto.Name });

            // Return a simple result
            return new SquirrelDto(squirrel.Id, squirrel.Name);
        }

        // Change a squirrel's name
        public SquirrelDto? UpdateSquirrelName(SquirrelUpdateDto updateDto)
        {
            // Look up the squirrel
            Squirrel? squirrel = _squirrelRepo.GetSquirrelById(updateDto.Id);
            if (squirrel == null) return null;

            // Save the new name
            Squirrel updatedSquirrel = _squirrelRepo.UpdateSquirrelName(squirrel, updateDto.Name);

            // Return a simple result
            return new SquirrelDto(updatedSquirrel.Id, updatedSquirrel.Name);
        }

        // Get all stashes for a squirrel
        public List<StashDto> GetAllStashes(int id) => _stashRepo.GetAllStashesBySquirrelId(id)
                .Select(s => new StashDto(
                    s.Id,
                    s.Location,
                    new SquirrelDto(s.Squirrel.Id, s.Squirrel.Name)))
                .ToList();

        // Delete a squirrel
        public bool DeleteSquirrel(int id)
        {
            // Look up the squirrel
            Squirrel? squirrel = _squirrelRepo.GetSquirrelById(id);
            if (squirrel == null) return false;

            // Remove it
            _squirrelRepo.DeleteSquirrel(squirrel);
            return true;
        }
    }
}
