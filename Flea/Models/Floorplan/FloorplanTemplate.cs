using System.Collections.Generic;

namespace Flea.Models.Floorplan
{
    public class FloorplanTemplate
    {
        public uint GridWidth { get; set; }
        public uint GridHeight { get; set; }

        public Dictionary<string, TableClass> TableClasses { get; set; } = null!;

    }
}