using SQLite;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    [Table("Tasks")]
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public bool Status { get; set; }
        public string FileName { get; set; }
        public string Coord { get; set; }
    }
}
