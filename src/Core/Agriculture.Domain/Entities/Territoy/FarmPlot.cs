using Agriculture.Domain.Enums;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Territoy
{
    public partial class FarmPlot : SoftDeletableEntity
    {
        public int FarmId { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public FarmPlotState State { get; private set; }

        public Farm Farm { get; private set; } = null!;
    }
}
