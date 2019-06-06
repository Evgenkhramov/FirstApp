using SQLite;
namespace FirstApp.Core.Entities
{ 
    [Table("Tasks")]
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }
}
