using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Views;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using Android.Locations;
using System.Linq;


namespace FirstApp.Core.Models
{

    public class Task
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public string[] FileName { get; set; }

        public MapCoord[] MarkerMapCoord = new MapCoord []{ } ;
    }

}
