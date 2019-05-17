using Newtonsoft.Json;
using System.Collections.Generic;

namespace FirstApp.Core.Models
{
    public class GoogleModeliOS
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        [JsonProperty(PropertyName = "emails")]
        public List<Email> Emails { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "name")]
        public Name UserName { get; set; }
        [JsonProperty(PropertyName = "image")]
        public Image UserImage { get; set; }

        public class Email
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }

        public class Name
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
}
