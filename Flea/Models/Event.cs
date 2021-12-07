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
        public string Name { get; set; }
        
        
        public List<Cluster> Clusters { get; set; }
        public List<Reservation> Reservations { get; set; }= new();
        public Event PreviousEvent { get; set; }


        /* the constructor creates the event and all the needed clusters in the bingo fleamarket formet*/
        public Event() {}
        public Event(DateTime dateTime)
        {
            DateTime = dateTime;
            UpdateName();

            Clusters = new List<Cluster>();
            // Top
            Clusters.Add(new Cluster("1", 2, 2, ClusterType.Vertical));
            Clusters.Add(new Cluster("2", 4, 4, ClusterType.Vertical));
            Clusters.Add(new Cluster("3", 4, 4, ClusterType.Vertical));
            Clusters.Add(new Cluster("4", 2, 2, ClusterType.Vertical));
            // Middle

            Clusters.Add(new Cluster("A", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("D", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("G", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("J", 4, 8, ClusterType.Round));

            Clusters.Add(new Cluster("B", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("E", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("H", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("K", 4, 8, ClusterType.Round));
            
            Clusters.Add(new Cluster("C", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("F", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("I", 4, 8, ClusterType.Round));
            Clusters.Add(new Cluster("L", 4, 8, ClusterType.Round));
            
            Clusters.Add(new Cluster("M", 3, 3, ClusterType.Vertical));
            Clusters.Add(new Cluster("N", 3, 3, ClusterType.Vertical));
            Clusters.Add(new Cluster("O", 3, 3, ClusterType.Vertical));
            Clusters.Add(new Cluster("P", 3, 3, ClusterType.Vertical));

            //Bottom
            Clusters.Add(new Cluster("5", 4, 4, ClusterType.Vertical));
            Clusters.Add(new Cluster("6", 3, 3, ClusterType.Vertical));
            Clusters.Add(new Cluster("7", 3, 3, ClusterType.Vertical));
        }

        public void UpdateName()
        {
            Name = DateTime.Day + ". " + _months[DateTime.Month] + ", " + DateTime.Year;
        }

        public Event Clone() => (Event) MemberwiseClone();

        public int ComputeMissingPayments => 
            Reservations.Aggregate(0, (acc, reservation) => reservation.PaymentStatus == PaymentStatus.Paid ? acc : acc + 1);

        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);
        DbSet<Event> IModelEntity<Event, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Events;
    }
}