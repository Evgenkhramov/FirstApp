using Newtonsoft.Json;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class GoogleModeliOS
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        [JsonProperty(PropertyName = "emails")]
        public List<GooglrModelEmail> Emails { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "name")]
        public GoogleModelUserName UserName { get; set; }
        [JsonProperty(PropertyName = "image")]
        public Image UserImage { get; set; }
    }
    public class GooglrModelEmail
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class GoogleModelUserName
    {
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
    }

    public class Image
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "isDefault")]
        public bool IsDefault { get; set; }
    }
}
