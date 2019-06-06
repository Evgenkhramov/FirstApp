using SQLite;

namespace FirstApp.Core.Entities
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
