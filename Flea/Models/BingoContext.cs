using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BingoContext : DbContext
    {
        public DbSet<Event>? Events { get; set; }
        
        public DbSet<Table>? Tables { get; set; }
        
        public DbSet<Reservation>? Reservations { get; set; }
        
        public DbSet<Customer>? Customers { get; set; }

        public BingoContext(DbContextOptions<BingoContext> contextOptions) : base(contextOptions)
        {
        }
    }
}