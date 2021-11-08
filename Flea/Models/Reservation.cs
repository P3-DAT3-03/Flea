using System.Collections.Generic;

namespace Flea.Models
{
    public class Reservation
    {
        /* Reservation class has public properties for, the priority of the reservation in regards to placement, 
         * the amount of table that need to be reserved, if they paid or not,
         * any comments, and a list of the tables that it has reserved.
         *TODO maybe add priority as Enum instead of as a int.
         *TODO consider if comments should be a list of strings and not one long string
         *TODO consider if Tables should be a array based on TableCount insted of a list.*/

        public int Priority { get; set; }
        public int TableCount { get; set; }
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public List<Table> Tables { get; } = new List<Table>();
        public Customer ReservationOwner { get; }

        public Reservation(int priority, int tableCount, bool paid, string comments, Customer reservationOwner)
        {
            Priority = priority;
            TableCount = tableCount;
            Paid = paid;
            Comments = comments;
            ReservationOwner = reservationOwner;
        }
    }
}