using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octanification.restClient
{
    class RestClient
    {
        HttpClient client;

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

        }

        public async Task<HttpResponseMessage> login(string userName, string pass)
        {
            var credentials = new Credentials() { user = userName, password = pass };
            
            return await Task.Run(() =>
            {
                return client.PostAsJsonAsync("authentication/sign_in", credentials);
            });
     
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
            return await Task.Run(() =>
            {
                return client.GetAsync(url);
            });
        }
    }
}
