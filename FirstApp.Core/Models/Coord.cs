namespace FirstApp.Core.Models
{
    public class MapCoord
    {
        public double Lng { get; set; }
        public double Lat { get; set; }

        public MapCoord(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }
    }

}