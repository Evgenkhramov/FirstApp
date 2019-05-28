using Newtonsoft.Json;
using System.Threading.Tasks;
using FirstApp.Core.Models;
using FirstApp.Core.Interfaces;
using System;
using System.Net.Http;
using Acr.UserDialogs;

namespace FirstApp.Core.Services
{
    public class FacebookService : IFacebookService
    {
        public async Task<FacebookModel> GetUserDataAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            string json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name,id,picture&access_token={accessToken}");
            FacebookModel user = JsonConvert.DeserializeObject<FacebookModel>(json);
            return user;
        }

        public async Task<string> GetImageFromUrlToBase64(string url)
        {
            string userPhoto = null;
            var httpClient = new HttpClient();
            try
            {
                var uri = new Uri(url);
                var response = httpClient.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        userPhoto = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch (Exception exeption)
            {
                UserDialogs.Instance.Alert(exeption.Message);
            }

            return userPhoto;
        }
    }
}
