using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class Event : IModelEntity<Event, BingoContext>
    {
        private readonly string[] _months =
        {
            "", "Januar", "Februar", "Marts", "April", "Maj", "Juni", "Juli", 
            "August", "September", "Oktober", "November", "December"
        };
        
        public int Id { get; set; }

        /// <summary>
        /// The date at which the event will be held.
        /// </summary>
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        
        public List<Cluster> Clusters { get; set; }
        public List<Reservation> Reservations { get; set; }= new();
        public Event PreviousEvent { get; set; }

        public Event()
        {
            
        }

        /* the constructor creates the event and all the needed clusters in the bingo fleamarket format*/
        public Event(DateTime dateTime)
        {
            DateTime = dateTime;
            UpdateName();
            
            Clusters = new List<Cluster>
            { 
                new("A", 4, 8), new("B", 4, 8), new("C", 4, 8), new("D", 4, 8),
                new("E", 4, 8), new("F", 4, 8), new("G", 4, 8), new("H", 4, 8),
                new("I", 4, 8), new("J", 4, 8), new("K", 4, 8), new("L", 4, 8),
                new("M", 12, 12)
            };
        }

        public void UpdateName()
        {
            Name = DateTime.Day + ". " + _months[DateTime.Month] + ", " + DateTime.Year;
        }

        public Event Clone() => (Event) MemberwiseClone();

        public int ComputeMissingPayments => 
            Reservations.Aggregate(0, (acc, reservation) => reservation.Paid ? acc : acc + 1);

        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);

        public DbSet<Event> GetDbSet(BingoContext ctx) => ctx.Events!;
    }
}