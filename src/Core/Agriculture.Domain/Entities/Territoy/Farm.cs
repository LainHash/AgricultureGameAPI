using Agriculture.Domain.Entities.Guest;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Territoy
{
    public class Farm : SoftDeletableEntity
    {
        public int PlayerId { get; set; }

        public string Name { get; set; } = null!;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Level { get; set; }

        public Player Player { get; set; } = null!;
        public ICollection<FarmPlot> FarmPlots { get; set; } = null!;
    }
}
