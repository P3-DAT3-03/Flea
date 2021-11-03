using System;
using System.Collections.Generic;
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
                { new Cluster("A", 4, 8), new Cluster("B", 4, 8), new Cluster("C", 4, 8), new Cluster("D", 4, 8) };
        }


        public int ComputeRemainingTables => Clusters.Aggregate(0, (acc, cluster) => acc + cluster.TablesNotPlaced);
    }

    public class Cluster
    {
        public string Name { get; set; }
        public Table[] Tables { get; set; }
        public int CustomerCount { get; set; }

        public Cluster(string name, int customerCount, int tableAmount)
        {
            Name = name;
            CustomerCount = customerCount;
            Tables = new Table[tableAmount];
            for (int i = 0; i < tableAmount; i++)
            {
                Tables[i] = new Table();
            }
        }

        public int ReservationCount =>
            Tables.Aggregate(0, (acc, table) => table.TableReservation != null ? acc + 1 : acc);

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