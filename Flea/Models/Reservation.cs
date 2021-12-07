using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public enum PaymentStatus
    {
        NotPaid,
        Paid,
        Other,
    }
    [Index("ReservationOwnerId", "EventId", IsUnique = true)]
    public class Reservation : IModelEntity<Reservation, BingoContext>, IValidatableObject
    {
        /*TODO maybe add priority as Enum instead of as a int.
         *TODO consider if comments should be a list of strings and not one long string
         *TODO consider if Tables should be a array based on TableCount instead of a list.*/

        public int Id { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have et antal stande.")]
        [Range(1, 3, ErrorMessage = "Prioteten skal være mellem {1} og {2}.")]
        public int Priority { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have et antal stande.")]
        [Range(1, 8, ErrorMessage = "Antal stande skal være mellem {1} og {2}.")]
        public int TableCount { get; set; }
        [Required(ErrorMessage = "En reservation skal have et antal tomme stande.")]
        public int EmptyTableCount { get; set; }
        
        [Required(ErrorMessage = "En reservation skal have en betalings status.")]
        public PaymentStatus PaymentStatus { get; set; }
        public string Comments { get; set; }
        public List<Table> Tables { get; set; } = new();
        
        public Event Event { get; set; }
        
        [ValidCustomer(ErrorMessage = "En reservation skal have en kunde.")]
        public Customer ReservationOwner { get; set; }

        public Reservation() {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="tableCount"></param>
        /// <param name="emptyTableCount"></param>
        /// <param name="status"></param>
        /// <param name="comments"></param>
        /// <param name="reservationOwner"></param>
        /// <param name="event"></param>
        public Reservation(int priority, int tableCount, PaymentStatus status, string comments, Customer reservationOwner, Event @event)
        {
            Priority = priority;
            TableCount = tableCount;
            PaymentStatus = status;
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
        public Reservation(int priority, int tableCount, PaymentStatus status, string comments)
        {
            Priority = priority;
            TableCount = tableCount;
            PaymentStatus = status;
            Comments = comments;
            
            Event = null!;
            ReservationOwner = null!;
        }

        public Reservation Clone() => (Reservation) MemberwiseClone();
        public DbSet<Reservation> GetDbSet(BingoContext ctx) => ctx.Reservations!;
        /// <summary>
        /// Used to validate fields that are dependent on each other
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if(TableCount - EmptyTableCount < 0)
                result.Add(new ValidationResult(
                    "Det kan ikke være flere tomme stande end resevered stande.",
                    new [] { "EmptyTableCount" }));
            return result;
        }
    }
    
}