using SQLite;

namespace FirstApp.Core.Entities
{
    [Table("FileName")]
    public class FileListEntity : BaseEntity
    {
        public int TaskId { get; set; }
        public string FileName { get; set; }
    }
}
