using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Octanification.restClient
{
    public class RestClient
    {
       /* [JsonConverter(typeof(EntityConverter))]
        public class Entity
        {
            public string name { get; set; }
            public string label { get; set; }
            public string type { get; set; }
            public string features { get; set; }
        }

        public class EntityConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Entity);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                    return null;
                var array = JArray.Load(reader);
                var entity = (existingValue as Entity ?? new Entity());
                entity.features = (string)array.ElementAtOrDefault(0);
                entity.name = (string)array.ElementAtOrDefault(1);
                entity.label = (string)array.ElementAtOrDefault(2);
                entity.type = (string)array.ElementAtOrDefault(3);
                return entity;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var entity = (Entity)value;
                serializer.Serialize(writer, new[] { entity.features, entity.name, entity.label, entity.type});
            }
        }*/

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

        public async Task<List<string>> getEntities()
        {
            List<string> entitiesLsit = new List<string>();
            Dictionary<string, string> mapEntity = new Dictionary<string, string>();
            List<Dictionary<string, string>> listOfEntities = new List<Dictionary<string, string>>();
            HttpResponseMessage response = await client.GetAsync(SHAREDSPACE_WORKSPACE_URL + "metadata/entities");
            if (response.StatusCode.ToString().Equals("OK"))
            {
                JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //var settings = new JsonSerializerSettings { Converters = new[] { new EntityConverter() } };
               // var list = JsonConvert.DeserializeObject<List<Entity>>(response.Content.ReadAsStringAsync().Result, settings);
                JToken entities = (json.First).First;
                foreach (JToken entity in entities)
                {
                    if (entity.ToString().Contains("business_rules"))
                    {
                        string label = (entity.First.Next.Next).First.ToString();
                        entitiesLsit.Add(label);
                    }
                }
            }
            return entitiesLsit;
        }
    }
}
