using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirstApp.Core.Services
{
    public class FacebookService : IFacebookService
    {
        public async Task<FacebookUserModel> GetUserDataAsync(string accessToken)
        {
            var httpClient = new HttpClient();

            string json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name,id,picture&access_token={accessToken}");

            FacebookUserModel user = JsonConvert.DeserializeObject<FacebookUserModel>(json);

            return user;
        }

        public async Task<string> GetImageFromUrlToBase64(string photoURL)
        {
            string userPhoto = null;
            var httpClient = new HttpClient();
            byte[] imageBytes = new byte[] { };

            var photoUri = new Uri(photoURL);
            HttpResponseMessage response = httpClient.GetAsync(photoUri).Result;

            if (response.IsSuccessStatusCode)
            {
                imageBytes = await response.Content.ReadAsByteArrayAsync();
            }

            if (imageBytes.Length > default(int))
            {
                userPhoto = Convert.ToBase64String(imageBytes);
            }

            return userPhoto;
        }
    }
}
