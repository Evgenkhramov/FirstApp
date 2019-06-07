using FirstApp.Core;
using FirstApp.Core.Interfaces;
using SQLite;
using System.IO;

namespace FirstApp.Droid.Services
{
    public class ConnectionService : IConnectionService
    {
        #region Methods

        public SQLiteConnection GetDatebaseConnection()
        {
            string dbName = Constants.NameDB;
            string path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
        }

        #endregion Methods
    }
}
