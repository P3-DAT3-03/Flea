namespace Flea.Models
{
    public class Table
    {
        /*table class contains a pointer TableReservation, to a reservation object or null and TableType thats a enum*/
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