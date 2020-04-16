using System;
using System.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            var longurl = hotspotSettings.Value.BaseAddress + uri;
            var httpClient = new HttpClient();
            var builder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            var response = await httpClient.GetAsync(urlfinal).Result.Content.ReadAsStringAsync();
            return response;
        }

        public async Task<string> PostAsync(string uri, string data)
        {
            var longurl = hotspotSettings.Value.BaseAddress + uri;
            var httpClient = new HttpClient();
            var builder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsync(urlfinal, new StringContent(data, Encoding.UTF8,
                                    "application/json")).Result.Content.ReadAsStringAsync();
            return response;

        }


        public async Task<string> DeleteAsync(string uri)
        {
            var longurl = hotspotSettings.Value.BaseAddress + uri;
            var httpClient = new HttpClient();
            var builder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            var response = await httpClient.DeleteAsync(urlfinal).Result.Content.ReadAsStringAsync();
            return response;

        }

        public async Task<string> PutAsync(string uri, string data)
        {
            var longurl = hotspotSettings.Value.BaseAddress + uri;
            var httpClient = new HttpClient();
            var builder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = hotspotSettings.Value.ApiKey;
            builder.Query = query.ToString();
            var urlfinal = builder.ToString();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PutAsync(urlfinal, new StringContent(data, Encoding.UTF8,
                                    "application/json")).Result.Content.ReadAsStringAsync();
            return response;

        }



    }
}
