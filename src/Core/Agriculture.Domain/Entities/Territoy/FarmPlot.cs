using Agriculture.Domain.Enums;
using Agriculture.Domain.Models;

namespace Agriculture.Domain.Entities.Territoy
{
    public class FarmPlot : SoftDeletableEntity
    {
        public int FarmId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public FarmPlotState State { get; set; }


        public Farm Farm { get; set; } = null!;
    }
}
