using System.IO;
using FirstApp.Core;
using FirstApp.Core.Interfaces;
using SQLite;

namespace FirstApp.Droid.Services
{
    public class DBConnectionService : IDBConnectionService
    {
        public SQLiteConnection connection;
        public DBConnectionService()
        {
            connection = GetDatebaseConnection();
        }

        public SQLiteConnection GetDatebaseConnection()
        {
            string dbName = Constants.NameDB;
            string path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
        }
    }
}
