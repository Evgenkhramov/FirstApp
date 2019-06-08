using SQLite;
namespace FirstApp.Core.Entities
{ 
    [Table("Tasks")]
    public class TaskEntity : BaseEntity
    {
        public int UserId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
    }
}
