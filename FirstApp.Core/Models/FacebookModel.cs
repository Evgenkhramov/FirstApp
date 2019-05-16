namespace FirstApp.Core.Models
{
    public class FacebookModel
    {
        public string Email { get; set; }
        public double Id { get; set; }
        public string First_name { get; set; } 
        public string Last_name { get; set; } 
        public Picture picture { get; set; }

        public class Data
        {
            public int height { get; set; }
            public bool is_silhouette { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }

        public class Picture
        {
            public Data data { get; set; }
        }
    }
}
