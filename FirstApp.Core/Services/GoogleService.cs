using FirstApp.Core.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FirstApp.Core.Services
{
    public class GoogleService
    {
        public async Task<GoogleModeliOS> GetUserProfileAsync(string tokenType, string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
            string json = await httpClient.GetStringAsync("https://www.googleapis.com/plus/v1/people/me?alt=json&access_token=" + accessToken);
            GoogleModeliOS user =  JsonConvert.DeserializeObject<GoogleModeliOS>(json);

            return user;
        }
    }
}
