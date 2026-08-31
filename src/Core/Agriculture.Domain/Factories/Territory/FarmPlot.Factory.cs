namespace Agriculture.Domain.Entities.Territoy
{
    public partial class FarmPlot
    {
        public FarmPlot() { }

        public FarmPlot SetFarm(int farmId)
        {
            FarmId = farmId;
            return this;
        }
    }
}
