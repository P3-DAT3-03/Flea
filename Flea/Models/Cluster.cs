using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public int CustomerCount { get; set; }
        public ClusterType Type { get; set; }

        public Cluster(string name, int customerCount, int tableAmount, ClusterType type)
        {
            Name = name;
            CustomerCount = customerCount;
            Tables = new List<Table> {Capacity = customerCount};
            Type = type;
            for (var i = 0; i < tableAmount; i++)
            {
                Tables.Add(new Table());
            }
        }

        public Cluster(string name, int customerCount)
        {
            Name = name;
            CustomerCount = customerCount;
        }

        public Cluster()
        {
            Id = 0;
        }

        public int ReservationCount =>
            Tables.Aggregate(0, (acc, table) => table.Reservation != null ? acc + 1 : acc);
        
        public int EmptyTableCount =>
            Tables.Aggregate(0, (acc, table) => table.Type == TableType.Empty ? acc + 1 : acc);

        public int TablesNotPlaced => Tables.Count - ReservationCount;

        /*TODO need to implement a function that checks if a placement is possible based on the cluster criterea,
         * fx if it sould cause too many customers to be seated*/
        public bool VerifyPlacements => throw new NotImplementedException();

        /*TODO this function needs to make it possible for the cluster to assign reservations to tables,
         * and verify if theyre correct based on a table and reservation as input*/
        public bool AssignReservationToTables => throw new NotImplementedException();
    }
}