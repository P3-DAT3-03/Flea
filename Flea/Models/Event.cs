using System;
using System.Collections.Generic;
using System.Linq;
using Flea.Models.Floorplan;
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
        public List<Reservation> Reservations { get; set; }
        public Event PreviousEvent { get; set; }

        public Event()
        {
            
        }

        /* the constructor creates the event and all the needed clusters in the bingo fleamarket formet*/
        public Event(string name, DateTime dateTime)
        {
            DateTime = dateTime;
            Name = name;
        }
        
        public Event(string name, DateTime dateTime, FloorplanTemplate template)
        {
            DateTime = dateTime;
            Name = name;
            Clusters = template.CreateClusters(4);
        }
        
        
        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);


        public DbSet<Event> GetDbSet(BingoContext ctx) => ctx.Events!;
    }
}