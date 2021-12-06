using System;
using Microsoft.EntityFrameworkCore;

namespace Flea.Models
{
    public enum TableType
    {
        Table,
        Empty,
        Rack,
    }
    public class Table :  IModelEntity<Table, BingoContext>
    {

        public int Id { get; set; }
        
        public Cluster Cluster { get; set; } = null!;

        public Reservation? Reservation { get; set; }
        public TableType Type { get; set; }

        public Table(TableType type = TableType.Table, Reservation? reservation = null)
        {
            Type = type;
            Reservation = reservation;
        }

        [Obsolete("This constructor should never be called manually. Intended only for EF use.")]
        public Table()
        {
            Id = 0;
        }

        DbSet<Table> IModelEntity<Table, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Tables;
    }
}