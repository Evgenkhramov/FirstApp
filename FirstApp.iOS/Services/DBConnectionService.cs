using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FirstApp.Core;
using FirstApp.Core.Interfaces;
using Foundation;
using SQLite;
using UIKit;

namespace FirstApp.iOS.Services
{
    class DBConnectionService : IDBConnectionService
    {
        public SQLiteConnection connection;

        public DBConnectionService()
        {
            connection = GetDatebaseConnection();
        }

        public SQLiteConnection GetDatebaseConnection()
        {
            string dbName = Constants.NameDB;
            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
        }
    }
}