using FirstApp.Core.Entities;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class MarkersMessage : MvxMessage
    {
        public List<MapMarkerEntity> MarkerMessegeList;

        public MarkersMessage(object sender, List<MapMarkerEntity> markerMessegeList)
           : base(sender)
        {
            MarkerMessegeList = markerMessegeList;
        }
    }
}
