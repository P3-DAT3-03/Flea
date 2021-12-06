using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; } = null!;


        public List<Cluster> Clusters { get; set; } = null!;
        public List<Reservation> Reservations { get; set; }= new();
        public Event PreviousEvent { get; set; }


        /* the constructor creates the event and all the needed clusters in the bingo fleamarket formet*/
        [Obsolete("This constructor should never be called manually. Intended only for EF use.")]
        public Event() {}
        public Event(DateTime dateTime)
        {
            DateTime = dateTime;
            UpdateName();

            Clusters = new List<Cluster>
            {
                // Top
                new ("1", 2, 2, ClusterType.Vertical),
                new ("2", 4, 4, ClusterType.Vertical),
                new ("3", 4, 4, ClusterType.Vertical),
                new ("4", 2, 2, ClusterType.Vertical),
                // Top Middle
                new ("M", 3, 3, ClusterType.Vertical),
                new ("N", 3, 3, ClusterType.Vertical),
                new ("O", 3, 3, ClusterType.Vertical),
                new ("P", 3, 3, ClusterType.Vertical),
                // Middle
                new ("A", 4, 8, ClusterType.Round),
                new ("D", 4, 8, ClusterType.Round),
                new ("G", 4, 8, ClusterType.Round),
                new ("J", 4, 8, ClusterType.Round),
                new ("B", 4, 8, ClusterType.Round),
                new ("E", 4, 8, ClusterType.Round),
                new ("H", 4, 8, ClusterType.Round),
                new ("K", 4, 8, ClusterType.Round),
                new ("C", 4, 8, ClusterType.Round),
                new ("F", 4, 8, ClusterType.Round),
                new ("I", 4, 8, ClusterType.Round),
                new ("L", 4, 8, ClusterType.Round),
                //Bottom
                new ("5", 4, 4, ClusterType.Vertical),
                new ("6", 3, 3, ClusterType.Vertical),
                new ("7", 3, 3, ClusterType.Vertical)
            };
        }

        public void UpdateName()
        {
            Name = DateTime.Day + ". " + _months[DateTime.Month] + ", " + DateTime.Year;
        }

        public Event Clone() => (Event) MemberwiseClone();

        public int ComputeMissingPayments => 
            Reservations.Aggregate(0, (acc, reservation) => reservation.PaymentStatus == PaymentStatus.NotPaid ? acc + 1 : acc);
        
        public int ComputeArrived => 
            Reservations.Aggregate(0, (acc, reservation) => reservation.Arrived ? acc + 1 : acc);
        public int ComputeReservationCount => 
            Reservations.Aggregate(0, (acc, _) =>  acc + 1);

        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);
        
        public int ComputeTableCount => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.Tables.Count);
        public int ComputeEmptyTableCount => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.EmptyTableCount);

        public IEnumerable<Table> AssignReservation(Cluster cluster, int[] tableIds, Reservation r)
        {
            var tables = cluster.Tables.FindAll(t => tableIds.Contains(t.Id));
            tables.ForEach(t=> t.Reservation = r);
            return tables;
        }
        DbSet<Event> IModelEntity<Event, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Events;
    }
}