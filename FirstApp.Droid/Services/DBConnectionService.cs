using FirstApp.Core;
using FirstApp.Core.Interfaces;
using SQLite;
using System.IO;

namespace FirstApp.Droid.Services
{
    public class DBConnectionService : IDBConnectionService
    {
        #region Variables

        public SQLiteConnection connection;

        #endregion Variables

        #region Constructors

        public DBConnectionService()
        {
            connection = GetDatebaseConnection();
        }

        #endregion Constructors

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
