using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class MarkersMessage : MvxMessage
    {
        public MarkersMessage(object sender, List<MapMarkerModel> mapMarkerList)
           : base(sender)
        {
            MapMarkerList = mapMarkerList;
        }

        public List<MapMarkerModel> MapMarkerList;
    }
}
