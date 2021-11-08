namespace Flea.Models
{
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
}