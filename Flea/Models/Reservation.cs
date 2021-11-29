using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    [Index("ReservationOwnerId", "EventId", IsUnique = true)]
    public class Reservation : IModelEntity<Reservation, BingoContext>
    {
        /*TODO maybe add priority as Enum instead of as a int.
         *TODO consider if comments should be a list of strings and not one long string
         *TODO consider if Tables should be a array based on TableCount instead of a list.*/

        public int Id { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have et antal borde.")]
        [Range(1, 3, ErrorMessage = "Prioteten skal være mellem {1} og {2}.")]
        public int Priority { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have et antal borde.")]
        [Range(1, 8, ErrorMessage = "Antal border skal være mellem {1} og {2}.")]
        public int TableCount { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have en betalings status.")]
        public bool Paid { get; set; }
        public string Comments { get; set; }
        public List<Table> Tables { get; set; } = new();
        
        public Event Event { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have en kunde.")]
        [ValidCustomer(ErrorMessage = "En reservation skal have en kunde.")]
        public Customer ReservationOwner { get; set; }

        public Reservation() {}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="tableCount"></param>
        /// <param name="paid"></param>
        /// <param name="comments"></param>
        /// <param name="reservationOwner"></param>
        /// <param name="event"></param>
        public Reservation(int priority, int tableCount, bool paid, string comments, Customer reservationOwner, Event @event)
        {
            Priority = priority;
            TableCount = tableCount;
            Paid = paid;
            Comments = comments;
            ReservationOwner = reservationOwner;
            Event = @event;
        }
        
        /// <summary>
        /// Constructor for use by the entity framework.
        /// This function should never be called.
        /// </summary>
        [Obsolete("This constructor should never be called manually. Intended only for EF use.")]
        // ReSharper disable once UnusedMember.Global
        public Reservation(int priority, int tableCount, bool paid, string comments)
        {
            Priority = priority;
            TableCount = tableCount;
            Paid = paid;
            Comments = comments;
            
            Event = null!;
            ReservationOwner = null!;
        }

        public Reservation Clone() => (Reservation) MemberwiseClone();
        public DbSet<Reservation> GetDbSet(BingoContext ctx) => ctx.Reservations!;
    }
}