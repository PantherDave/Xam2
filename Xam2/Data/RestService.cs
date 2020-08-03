using System;
using Xam2.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Xam2.Models;

namespace Xam2.Data
{
    public class RestService
    {
        HttpClient client;
        string grant_type = "password";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers
                .MediaTypeWithQualityHeaderValue("application/x-www-" +
                "fore-urlencoded"));

        }

        public async Task<Token> Login(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type",
                grant_type));
            postData.Add(new KeyValuePair<string, string>("username",
                user.Username));
            postData.Add(new KeyValuePair<string, string>("password",
                user.Password));
            var weburl = Constants.LogInUrl;
            var content = new FormUrlEncodedContent(postData);
            var response = await PostResponseLogin<Token>(weburl, content);
            DateTime dt = DateTime.Today;
            response.Expire_date = dt.AddSeconds(100);
            return response;
        }

        public async Task<T> PostResponseLogin<T>(string weburl,
            FormUrlEncodedContent content) where T : class
        {
            var response = await client.PostAsync(weburl, content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<T>(jsonResult);
            return token;
        }

        public async Task<T> PostResponse<T>(string weburl, string json)
            where T : class
        {
            var token = App.TokenDatabase.GetToken();
            string contentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new System
                .Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                token.Access_token);
            try
            {
                var result = await client.PostAsync(weburl, new StringContent(json,
                        Encoding.UTF8, contentType));
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var contentResp = JsonConvert.DeserializeObject<T>(jsonResult);
                        return contentResp;
                    }
                    catch {return null;}
                }
            }
            catch {return null;}
            return null;
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            var token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new
                System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                token.Access_token);
            try
            {
                var response = await client.GetAsync(weburl);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var contentResp = JsonConvert.DeserializeObject<T>(jsonResult);
                        return contentResp;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }
    }
}
