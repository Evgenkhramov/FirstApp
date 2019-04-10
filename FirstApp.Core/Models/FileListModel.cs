using SQLite;

namespace FirstApp.Core.Models
{
    [Table("FileName")]
    public class FileListModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string FileName { get; set; }
    }
}
