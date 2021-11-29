using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Flea.Models.Floorplan
{
    
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public class FloorplanTemplate
    {
        public uint GridWidth { get; set; }
        public uint GridHeight { get; set; }

        public Dictionary<string, TableClass> TableClasses { get; set; } = null!;

        public Dictionary<string, TableDefinition[]> Clusters { get; set; } = null!;

        public IEnumerable<TableDefinition> Tables => Clusters.SelectMany(cluster => cluster.Value);

        public TableClass GetTableClass(TableDefinition table) => TableClasses[table.Class];

        public TableDefinition GetTable(Table table) => Clusters[table.Cluster.Name][table.ClusterIndex];

        /// <summary>
        /// Instantiates a list of <see cref="Cluster"/> instances based on
        /// the floorplan template.
        /// </summary>
        /// <param name="customerCount"></param>
        /// <returns></returns>
        public List<Cluster> CreateClusters(int customerCount)
        {
            List<Cluster> cluster = new();
            return Clusters.Select(pair =>
                new Cluster(
                    pair.Key, customerCount,
                    pair.Value
                        .Select((tableDef, i) => tableDef.AsTable(i))
                        .ToList()
                )
            ).ToList();
        }
    }
}