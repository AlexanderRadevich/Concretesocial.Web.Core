using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Concretesocial.Web.Core
{
    public static class ConcretesocialCore
    {
        const string baseApiURL = "https://concretesocial.io/1.0/";

        public static T MakeRequest<T>(string api, string clientID, string clientSecret, object data = null, bool userUrlForAuthorization = false) where T : class
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "Concretesocial.Core TestApp");
            if(!userUrlForAuthorization)
            {
                client.DefaultRequestHeaders.Add("client_id", clientID);
                client.DefaultRequestHeaders.Add("client_secret", clientSecret);
            }

            HttpResponseMessage response = null;
            string url = $"{baseApiURL}{api}";
         
            if(userUrlForAuthorization)
            {
                url = $"{url}&client_id={clientID}&client_secret={clientSecret}";
            }

            if (data != null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                response = client.PostAsync(url, content).Result;
            }
            else
            {
                response = client.GetAsync(url).Result;
            }

           
            var responseData = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseData);
            }
            else
            {
                throw new Exception($"Cannot process an api request: {responseData}");
            }
        }

    }

    #region API Objects

    public class CodeAuthorizationResponse
    {
        public ProfileItem[] Profiles { get; set; }
    }

    public class ProfileItem
    {
        public string id { get; set; }
        public int followers_count { get; set; }
        public int follows_count { get; set; }
        public int media_count { get; set; }
        public string custom_id { get; set; }
        public string user { get; set; }

        public override string ToString()
        {
            return $"id: {id}, user: {user}, followers_count: {followers_count}, follows_count: {follows_count}, media_count: {media_count}";
        }
    }

    public class MediaRequestItem
    {
        public string caption { get; set; }
        public string media_type { get; set; }
        public string media_url { get; set; }
        public string[] profiles { get; set; }
        public string comment { get; set; }
    }

    public class APIResponseItem
    {
        public string request_id { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public string profile { get; set; }
        public string media { get; set; }
        public Response response { get; set; }
    }

    public class Response
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string error_user_title { get; set; }
        public string error_user_msg { get; set; }

        public override string ToString()
        {
            return $"{message} {Environment.NewLine} {error_user_title} {Environment.NewLine} {error_user_msg}";
        }
    }

    #endregion
}
