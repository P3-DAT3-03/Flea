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
        
        public Cluster Cluster { get; set; }
        
        public Reservation? Reservation { get; set; }
        public TableType Type { get; set; }

        public Table(TableType type = TableType.Table, Reservation? reservation = null)
        {
            Type = type;
            Reservation = reservation;
        }

        public Table(TableType type)
        {
            Type = type;
        }

        public Table()
        {
            Id = 0;
        }

        DbSet<Table> IModelEntity<Table, BingoContext>.GetDbSet(BingoContext ctx) => ctx.Tables;
    }
}