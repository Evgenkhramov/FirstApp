using FirstApp.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.Services
{
    public class GoogleService
    {

        public async Task<string> GetEmailAsync(string tokenType, string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
            var json = await httpClient.GetStringAsync("https://www.googleapis.com/userinfo/email?alt=json");
            var email = JsonConvert.DeserializeObject<GoogleEmail>(json);

            return email.Data.Email;
        }

    }
}
