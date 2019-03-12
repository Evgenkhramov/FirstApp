using FirstApp.Core.Interfaces;
using MvvmCross;
using SQLite;
using System;
using FirstApp.Core;
using FirstApp.Core.Models;
using System.Collections.Generic;
using System.Text;
using FirstApp.Core.Services;
using System.Linq;


namespace FirstApp.Core.Services
{
    public class SQLiteRepository : ISQLiteRepository
    {
        SQLiteConnection database;
        public SQLiteRepository()
        {
            string filename = Constants.NameDB;
            string databasePath = Mvx.IoCProvider.Resolve<ISQliteAddress>().GetDatabasePath(filename);

            database = new SQLiteConnection(databasePath);

            database.CreateTable<UserDatabaseModel>();
        }

        //public UserDatabaseModel GetItems(int id)
        //{
        //    return database.Table<UserDatabaseModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        //}

        public IEnumerable<UserDatabaseModel> GetItems()
        {
            return (from i in database.Table<UserDatabaseModel>() select i).ToList();
        }
        public UserDatabaseModel GetItem(int id)
        {
            return database.Get<UserDatabaseModel>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<UserDatabaseModel>(id);
        }
        public int SaveItem(UserDatabaseModel item)
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

