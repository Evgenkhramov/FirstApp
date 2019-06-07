using FirstApp.Core;
using FirstApp.Core.Interfaces;
using SQLite;
using System.IO;

namespace FirstApp.iOS.Services
{
    class DBConnectionService : IConnectionService
    {
        public SQLiteConnection GetDatebaseConnection()
        {
            string dbName = Constants.NameDB;
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
        }
    }
}