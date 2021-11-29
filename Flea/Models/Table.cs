using System.ComponentModel.DataAnnotations.Schema;

namespace Flea.Models
{
    public class Table
    {
        public enum TableType
        {
            Table,
            Empty,
            Rack,
        }
        
        public int Id { get; set; }
        
        public Cluster Cluster { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int ClusterIndex { get; set; }
        
        public Reservation? Reservation { get; set; }
        public TableType Type { get; set; }

        [NotMapped]
        public string Name => $"{Cluster.Name}{ClusterIndex}";
        
        public Table(int clusterIndex, TableType type = TableType.Table, Reservation? reservation = null)
        {
            ClusterIndex = clusterIndex;
            Cluster = null!;
            Type = type;
            Reservation = reservation;
        }
    }
}