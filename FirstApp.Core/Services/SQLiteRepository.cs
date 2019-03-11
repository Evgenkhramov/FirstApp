using FirstApp.Core.Interfaces;
using MvvmCross;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Services;
using System.Linq;


namespace FirstApp.Core.Services
{
    public class SQLiteRepository
    {
        SQLiteConnection database;
        public SQLiteRepository(string filename)
        {
            string databasePath = Mvx.Resolve<ISQliteAddress>().GetDatabasePath(filename);


            database = new SQLiteConnection(databasePath);

            database.CreateTable<Friend>();
        }
        public IEnumerable<Friend> GetItems()
        {
            return (from i in database.Table<Friend>() select i).ToList();

        }
        public Friend GetItem(int id)
        {
            return database.Get<Friend>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Friend>(id);
        }
        public int SaveItem(Friend item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }

}

