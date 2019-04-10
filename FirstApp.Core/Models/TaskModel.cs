using SQLite;

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
        public string MarkerMap  { get; set; }
    }
}
