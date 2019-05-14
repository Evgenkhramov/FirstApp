using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Models
{
    public class GoogleModeliOS
    {

        public string Kind { get; set; }
        public string Etag { get; set; }
        public List<Email> Emails { get; set; }
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Name UserName { get; set; }
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
            public string Url { get; set; }
            public bool IsDefault { get; set; }
        }
    }
}
