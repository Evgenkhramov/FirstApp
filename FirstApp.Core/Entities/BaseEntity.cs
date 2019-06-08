using SQLite;

namespace FirstApp.Core.Entities
{
    public class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
