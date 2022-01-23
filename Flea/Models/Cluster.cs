using System;
using System.Collections.Generic;
using System.Linq;

namespace Flea.Models
{
    public enum ClusterType
    {
        Round,
        Vertical,
        Horizontal,
    }
    public class Cluster
    {

        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
        public List<Table> Tables { get; set; } = null!;
        public int CustomerCount { get; set; }
        public ClusterType Type { get; set; }

        public Cluster(string name, int customerCount, int tableCount, ClusterType type)
        {
            Name = name;
            CustomerCount = customerCount;
            Tables = new List<Table> {Capacity = tableCount};
            Type = type;
            for (var i = 0; i < tableCount; i++)
            {
                Tables.Add(new Table(TableType.Table));
            }
        }
        
        [Obsolete("This constructor should never be called manually. Intended only for EF use.")]
        public Cluster(string name, int customerCount)
        {
            Name = name;
            CustomerCount = customerCount;
        }

        public int ReservationCount =>
            Tables.GroupBy(t => t.Reservation).Select(g => g.First()).Count()-1;

        public int ReservedTableCount => Tables.Aggregate(0, (i, table) => table.Reservation != null ? i + 1 : i);
        
        public int EmptyTableCount =>
            Tables.Aggregate(0, (acc, table) => table.Type == TableType.Empty ? acc + 1 : acc);

        public int TablesNotPlaced => Tables.Count - ReservedTableCount;

        /*TODO need to implement a function that checks if a placement is possible based on the cluster criterea,
         * fx if it sould cause too many customers to be seated*/
        public bool VerifyPlacements =>  ReservationCount <= CustomerCount;
    }
}