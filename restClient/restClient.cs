using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Octanification.restClient
{
    public class RestClient
    {
        HttpClient client;
        Credentials credentials;
        string HPSSO_COOKIE_CSRF;
        string HPSSO_HEADER_CSRF;
        string LWSSO_COOKIE_KEY;
        string workSpace;
        string sharedSpace;
        string SHAREDSPACE_WORKSPACE_URL;

        class Credentials
        {
            public string user { get; set; }
            public string password { get; set; }
        }

        public RestClient(String baseUri)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUri + "/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("HPECLIENTTYPE", "HPE_MQM_UI");
            workSpace = "2032";
            sharedSpace = "1001";
            SHAREDSPACE_WORKSPACE_URL = "api/shared_spaces/" + sharedSpace + "/workspaces/" + workSpace + "/";
        }

        public async Task<HttpResponseMessage> login(string userName, string pass)
        {
            credentials = new Credentials() { user = userName, password = pass };
            HttpResponseMessage response = await client.PostAsJsonAsync("authentication/sign_in", credentials);
            return await Task.Run(() =>
            {
                string[] cookies = response.Headers.GetValues("Set-Cookie").Cast<string>().ToArray();

                foreach(string header in cookies)
                {
                    if (header.IndexOf("HPSSO_COOKIE_CSRF") != -1)
                    {
                        HPSSO_COOKIE_CSRF = header;
                        HPSSO_HEADER_CSRF = header.Substring(18, 26);
                    }
                    else if (header.IndexOf("LWSSO_COOKIE_KEY") != -1)
                    {
                        LWSSO_COOKIE_KEY = header;
                    }
                    
                }
                addCookiesToHeaders();
                return response;
            });
     
        }

        private void addCookiesToHeaders()
        {
            client.DefaultRequestHeaders.Add("HPSSO_COOKIE_CSRF", HPSSO_COOKIE_CSRF);
            client.DefaultRequestHeaders.Add("HPSSO_HEADER_CSRF", HPSSO_HEADER_CSRF);
            client.DefaultRequestHeaders.Add("LWSSO_COOKIE_KEY", LWSSO_COOKIE_KEY);
        }

        public async Task<HttpResponseMessage> post(string url, string body)
        {
            return await Task.Run(() =>
            {
                return client.PostAsJsonAsync(url, body);
            });
        }

        public async Task<HttpResponseMessage> get(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            return await Task.Run(() =>
            {
                return client.GetAsync(url);
            });
        }

        public async Task<List<Dictionary<string, string>>> getWorkspaceUsers()
        {
            List<Dictionary<string, string>> listOfUsers = new List<Dictionary<string, string>>();
            Dictionary<string, string> mapUser = new Dictionary<string, string>();
            HttpResponseMessage response = await client.GetAsync(SHAREDSPACE_WORKSPACE_URL + "workspace_users");
            if (response.StatusCode.ToString().Equals("OK"))
            {
                string stringUsers = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(stringUsers);
                JToken users = (json.First.Next).First;
                foreach (JToken user in users)
                {
                    mapUser = user.ToObject<Dictionary<string, string>>();
                    listOfUsers.Add(mapUser);
                }
            }
            return listOfUsers;
        }
    }
}
