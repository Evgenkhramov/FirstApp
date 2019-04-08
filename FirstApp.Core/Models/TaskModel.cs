using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Locations;
using System.Linq;


namespace FirstApp.Core.Models
{

    public class TaskModel
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool Status { get; set; }

        public string[] FileName { get; set; }

        public MapCoord[] MarkerMapCoord = new MapCoord[] { };
    }

}
