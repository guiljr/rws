using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Http
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public IAuthClient(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("AppConfig:API_AUTH_BASE_URL"));
            //httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            var basicAuthUserName = _configuration.GetValue<string>("BasicAuth:UserName");
            var basicAuthPassword = _configuration.GetValue<string>("BasicAuth:Password");
            var encoded = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(basicAuthUserName + ":" + basicAuthPassword));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
            _client = httpClient;
        }

        //public Task<HttpResponseMessage> ContactsDetailsUpdate(Models.ContactModel model)
        //{
        //    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8);
        //    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    return _client.PostAsync("/api/contactsupdate", httpContent);
        //}

        public Task<HttpResponseMessage> GetUsers()
        {
            return _client.GetAsync($"/api/getusers");
        }

    }
}