using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Models
{
    [Table("MapMarker")]
    public class MapMarkerModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
