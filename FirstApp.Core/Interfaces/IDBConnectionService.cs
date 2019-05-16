using SQLite;

namespace FirstApp.Core.Interfaces
{
    public interface  IDBConnectionService
    {       
        SQLiteConnection GetDatebaseConnection();
    }
}

