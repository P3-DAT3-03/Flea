﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public class Event : IModelEntity<Event, BingoContext>
    {
        
        public string[] Months =
        {
            "Januar", "Februar", "Marts", "April", "Maj", "Juni", "Juli", 
            "August", "September", "Oktober", "November", "December"
        };
        
        public int Id { get; set; }

        /// <summary>
        /// The date at which the event will be held.
        /// </summary>
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        
        public List<Cluster> Clusters { get; set; }
        public List<Reservation> Reservations = new();
        public Event PreviousEvent { get; set; }

        public Event()
        {
            
        }

        /* the constructor creates the event and all the needed clusters in the bingo fleamarket format*/
        public Event(DateTime dateTime)
        {
            DateTime = dateTime; 
            Name = dateTime.Day.ToString() + ". " + Months[dateTime.Month] + ", " + dateTime.Year.ToString();

            Clusters = new List<Cluster>
            { 
                new Cluster("A", 4, 8), new Cluster("B", 4, 8), new Cluster("C", 4, 8), new Cluster("D", 4, 8),
                new Cluster("E", 4, 8), new Cluster("F", 4, 8), new Cluster("G", 4, 8), new Cluster("H", 4, 8),
                new Cluster("I", 4, 8), new Cluster("J", 4, 8), new Cluster("K", 4, 8), new Cluster("L", 4, 8),
                new Cluster("M", 12, 12)
            };
        }

        public int ComputeMissingPayments => 
            Reservations.Aggregate(0, (acc, reservation) => reservation.Paid ? acc : acc + 1);

        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);


        public DbSet<Event> GetDbSet(BingoContext ctx) => ctx.Events!;
    }
}