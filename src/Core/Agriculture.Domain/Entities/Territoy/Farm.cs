using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Territoy
{
    public class Farm : SoftDeletableEntity
    {
        public int PlayerId { get; private set; }

        public string Name { get; private set; } = null!;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Level { get; private set; }

        public ICollection<FarmPlot> FarmPlots { get; private set; } = null!;
        public ICollection<PlayerFarm> PlayerFarms { get; private set; } = [];
    }
}
