using SQLite;

namespace FirstApp.Core.Interfaces
{
    public interface  IConnectionService
    {       
        SQLiteConnection GetDatebaseConnection();
    }
}

