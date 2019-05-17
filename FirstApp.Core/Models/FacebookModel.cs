using Newtonsoft.Json;

namespace FirstApp.Core.Models
{
    public class FacebookModel
    {
        public string Email { get; set; }
        public double Id { get; set; }
        public string First_name { get; set; } 
        public string Last_name { get; set; }
        [JsonProperty(PropertyName = "picture")]
        public Picture UserPicture { get; set; }

        public class Data
        {
            [JsonProperty(PropertyName = "height")]
            public int Height { get; set; }
            [JsonProperty(PropertyName = "is_silhouette")]
            public bool IsSilhouette { get; set; }
            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }
            [JsonProperty(PropertyName = "width")]
            public int Width { get; set; }
        }

        public class Picture
        {
            [JsonProperty(PropertyName = "data")]
            public Data PictureData { get; set; }
        }
    }
}

