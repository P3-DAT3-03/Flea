using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flea.Models
{
    public class Reservation
    {
        /*TODO maybe add priority as Enum instead of as a int.
         *TODO consider if comments should be a list of strings and not one long string
         *TODO consider if Tables should be a array based on TableCount instead of a list.*/

        public int Id { get; set; }
        public int Priority { get; set; }
        public int TableCount { get; set; }
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public List<Table> Tables { get; } = new List<Table>();
        
        public Event Event { get; set; }
        public Customer ReservationOwner { get; set; }

        public Reservation(int priority, int tableCount, bool paid, string comments, Customer reservationOwner)
        {
            Priority = priority;
            TableCount = tableCount;
            Paid = paid;
            Comments = comments;
            ReservationOwner = reservationOwner;
        }
        
        public Reservation(int priority, int tableCount, bool paid, string comments)
        {
            Priority = priority;
            TableCount = tableCount;
            Paid = paid;
            Comments = comments;
        }

        public Reservation Clone() => (Reservation) this.MemberwiseClone();
    }
}