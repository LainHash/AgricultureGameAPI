namespace Agriculture.Seeding.DataRecords.Territory
{
    internal class FarmRecord
    {
        public string Name { get; set; } = null!;
        public int Width { get; set; }
        public int Height { get; set; }
        public int RequiredLevel { get; set; }
        public int RequiredCoin { get; set; }
    }
}
