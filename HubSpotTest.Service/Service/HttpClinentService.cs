using System;
using System.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Microsoft.Extensions.Options;

namespace HubSpotTest.Service.Service
{
    public class HttpClinentService: IHttpClinentService
    {
        private readonly IOptions<HubSpotSettings> hotspotSettings;

        public HttpClinentService(IOptions<HubSpotSettings> hotspotSettings)
        {
            this.hotspotSettings = hotspotSettings;
        }


        public async Task<string> GetAsync(string uri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(hotspotSettings.Value.BaseAddress);
            var builder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            var response = await httpClient.GetAsync(urlfinal).Result.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<string> PostAsync(string uri, string data)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(hotspotSettings.Value.BaseAddress);
            var builder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            var response = await httpClient.PostAsync(urlfinal, new StringContent(data)).Result.Content.ReadAsStringAsync();
            return response;

        }



    }
}
