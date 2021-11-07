using System;
using System.Linq;

namespace Flea.Models
{
    public class Event
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Cluster[] Clusters { get; set; }
        public Event PreviousEvent { get; set; }

        public Event(string name, DateTime dateTime)
        {
            DateTime = dateTime;
            Name = name;

            Clusters = new Cluster[]
                { 
                new Cluster("A", 4, 8), new Cluster("B", 4, 8), new Cluster("C", 4, 8), new Cluster("D", 4, 8),
                new Cluster("E", 4, 8), new Cluster("F", 4, 8), new Cluster("G", 4, 8), new Cluster("H", 4, 8),
                new Cluster("I", 4, 8), new Cluster("J", 4, 8), new Cluster("K", 4, 8), new Cluster("L", 4, 8),
                new Cluster("M", 12, 12)
                };
        }


        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);
    }
}