namespace Flea.Models.Floorplan
{
    public class TableClass
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

        public void Deconstruct(out uint width, out uint height)
        {
            width = Width;
            height = Height;
        }
    }
}