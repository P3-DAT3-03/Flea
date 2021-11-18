using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public class Event : IModelEntity<Event, BingoContext>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        /// <summary>
        /// The date at which the event will be held.
        /// </summary>
        public DateTime DateTime { get; set; }
        
        public List<Cluster> Clusters { get; set; }
        public List<Reservation> Reservations;
        public Event PreviousEvent { get; set; }

        public Event()
        {
            
        }

        /* the constructor creates the event and all the needed clusters in the bingo fleamarket formet*/
        public Event(string name, DateTime dateTime)
        {
            DateTime = dateTime;
            Name = name;

            Clusters = new List<Cluster>
                { 
                new Cluster("A", 4, 8), new Cluster("B", 4, 8), new Cluster("C", 4, 8), new Cluster("D", 4, 8),
                new Cluster("E", 4, 8), new Cluster("F", 4, 8), new Cluster("G", 4, 8), new Cluster("H", 4, 8),
                new Cluster("I", 4, 8), new Cluster("J", 4, 8), new Cluster("K", 4, 8), new Cluster("L", 4, 8),
                new Cluster("M", 12, 12)
                };
        }


        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);


        public DbSet<Event> GetDbSet(BingoContext ctx) => ctx.Events!;
    }
}