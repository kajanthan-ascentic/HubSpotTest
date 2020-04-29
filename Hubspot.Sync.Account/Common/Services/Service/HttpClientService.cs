using Hubspot.Sync.Account.Common.Models.Hubspot;
using Hubspot.Sync.Account.Common.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hubspot.Sync.Account.Common.Services.Service
{
    public class HttpClientService<T> : IHttpClientService<T>
    {
        private readonly HttpClient _httpClient = null;
        private readonly string _apiKey = String.Empty;

        public HttpClientService(string apiKey) 
        {
            this._apiKey = apiKey;
            
            this._httpClient = new HttpClient();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string path)
        {
            string result = await this.GetAsyncAsString(path);
            return JsonConvert.DeserializeObject<ListModel<T>>(result).results;
        }

        public async Task<T> GetAsync(string path)
        {
            string result = await this.GetAsyncAsString(path);
            return this.ConvertToModel(result);
        }

        public async Task<string> GetAsyncAsString(string path)
        {
            // get response
            HttpResponseMessage responseMessage = await this._httpClient.GetAsync(this.GetRequestUrl(path));

            // check if response exists
            if (responseMessage != null)
            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);
            }

            // pass null if failed
            return null;
        }

        public async Task<string> PostAsyncAsString(string path, object model)
        {
            // convert model to json content
            string json = JsonConvert.SerializeObject(model);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            // get response
            HttpResponseMessage responseMessage = await this._httpClient.PostAsync(this.GetRequestUrl(path), data);

            // check if response exists
            if (responseMessage != null)
            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);
            }

            return null;
        }

        public async Task<T> PostAsync(string path, object model)
        {
            string result = await this.PostAsyncAsString(path, model);
            return this.ConvertToModel(result);
        }


        private async Task<string> GetResponseAsString(HttpResponseMessage responseMessage)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();

            // check if null
            if (!String.IsNullOrEmpty(result))
            {
                // check if success
                if (responseMessage.IsSuccessStatusCode)
                {
                    return result;
                }
            }

            throw new Exception(result);
        }

        private T ConvertToModel(string content) 
        {
            //convert to model
            return JsonConvert.DeserializeObject<T>(content);
        }

        private string GetRequestUrl(string url) 
        {
            //append api key
            UriBuilder builder = new UriBuilder(Constants.URL_API  + url);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["hapikey"] = this._apiKey;
            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}
