using System;
using System.Linq;

namespace Flea.Models
{
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
            Tables.Aggregate(0, (acc, table) => table.Reservation != null ? acc + 1 : acc);

        public int TablesNotPlaced => Tables.Length - ReservationCount;

        /*TODO need to implement a function that checks if a placement is possible based on the cluster criterea,
         * fx if it sould cause too many customers to be seated*/
        public bool VerifyPlacements => throw new NotImplementedException();

        /*TODO this function needs to make it possible for the cluster to assign reservations to tables,
         * and verify if theyre correct based on a table and reservation as input*/
        public bool AssignReservationToTables => throw new NotImplementedException();
    }
}