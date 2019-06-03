using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class MarkersMessage : MvxMessage
    {
        public MarkersMessage(object sender, List<MapMarkerModel> markerMessegeList)
           : base(sender)
        {
            MarkerMessegeList = markerMessegeList;
        }
        
        public List<MapMarkerModel> MarkerMessegeList;
    }
}
