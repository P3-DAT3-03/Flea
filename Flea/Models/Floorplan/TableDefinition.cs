namespace Flea.Models.Floorplan
{
    public class TableDefinition
    {
        public string Class { get; set; }
        public uint X { get; set; }
        public uint Y { get; set; }

        public Table AsTable(int index) => new Table(index);
    }
}