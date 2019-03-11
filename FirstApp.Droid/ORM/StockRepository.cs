using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FirstApp.Droid.ORM
{
    public class StockRepository
    {
        StockDatabase db = null;
        protected static StockRepository me;
        static StockRepository()
        {
            me = new StockRepository();
        }
        protected StockRepository()
        {
            db = new StockDatabase(StockDatabase.DatabaseFilePath);
        }

        public static Stock GetStock(int id)
        {
            return me.db.GetStock(id);
        }

        public static IEnumerable<Stock> GetStocks()
        {
            return me.db.GetStocks();
        }

        public static int SaveStock(Stock item)
        {
            return me.db.SaveStock(item);
        }

        public static int DeleteStock(Stock item)
        {
            return me.db.DeleteStock(item);
        }
    }
}