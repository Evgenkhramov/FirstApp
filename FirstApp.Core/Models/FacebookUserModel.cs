using Newtonsoft.Json;

namespace FirstApp.Core.Models
{
    public class FacebookUserModel
    {
        public string Email { get; set; }
        public double Id { get; set; }
        [JsonProperty(PropertyName = "First_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "Last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "picture")]
        public FacebookModelPicture UserPicture { get; set; }   
    }
    public class FacebookModelPicture
    {
        [JsonProperty(PropertyName = "data")]
        public FacebookModelData PictureData { get; set; }
    }

    public class FacebookModelData
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
}

