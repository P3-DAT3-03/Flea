namespace Flea.Models.Floorplan
{
    public class TableDefinition
    {
        public string Class { get; set; }
        public uint X { get; set; }
        public uint Y { get; set; }

        public bool IsInBounds(float x, float y, FloorplanTemplate template)
        {
            var (width, height) = template.GetTableClass(this);
            return X <= x && x <= X + width && Y <= y && y <= Y + height;
        }  
        
        public Table AsTable(int index) => new Table(index);
    }
}