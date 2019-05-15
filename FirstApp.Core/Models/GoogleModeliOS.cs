using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Models
{
    public class GoogleModeliOS
    {

        public string Kind { get; set; }
        public string Etag { get; set; }
        public List<Email> emails { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Name name { get; set; }
        public Image image { get; set; }

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
            public string url { get; set; }
            public bool isDefault { get; set; }
        }
    }
}
