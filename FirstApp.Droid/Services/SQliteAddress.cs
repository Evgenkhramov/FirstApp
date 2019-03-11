using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FirstApp.Core.Interfaces;

namespace FirstApp.Droid.Services
    {
    public class SQliteAddress : ISQliteAddress
    {
        public SQliteAddress() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}
