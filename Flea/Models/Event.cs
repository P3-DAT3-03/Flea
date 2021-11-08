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


        /* the constructor creates the event and all the needed clusters in the bingo fleamarket formet*/
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


        public int TablesNotPlaced => Tables.Length - ReservationCount;
        public bool VerifyPlacements => throw new NotImplementedException();
        public bool AssignReservationToTables => throw new NotImplementedException();
    }

    public class Table
    {
        public enum TableType
        {
            table,
            empty,
            rack,
        }

        public Reservation TableReservation { get; set; }
        public TableType Type { get; set; }

        public Table()
        {
            Type = TableType.table;
        }
    }

    public class Reservation
    {
        public int Priority { get; set; }
        public int TableCount { get; set; }
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public List<Table> Tables { get; } = new List<Table>();
    }

    public class Customer
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
    }
}