using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HubSpot.Sync.Service.HttpClientData
{
    public class HttpClientService: IHttpClientService
    {
        private readonly HttpClient _httpClient = null;        public HttpClientService()        {            this._httpClient = new HttpClient();        }

        #region Request Methods
        public async Task<T> GetAsync<T>(string url)        {            string result = await this.GetAsyncAsString(url);            return this.ConvertToModel<T>(result);        }        public async Task<string> GetAsyncAsString(string url)        {
            // get response
            HttpResponseMessage responseMessage = await this._httpClient.GetAsync(url);

            // check if response exists
            if (responseMessage != null)            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);            }            throw new HttpRequestException("Http Response message null");        }        public async Task<string> PostAsyncAsString(string url, object model)        {
            // get response
            HttpResponseMessage responseMessage = await this._httpClient.PostAsync(url, this.GetStringContent(model));

            // check if response exists
            if (responseMessage != null)            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);            }            throw new HttpRequestException("Http Response message null");        }        public async Task<T> PostAsync<T>(string url, object model)        {            string result = await this.PostAsyncAsString(url, model);            return this.ConvertToModel<T>(result);        }        public async Task<T> PutAsync<T>(string url, object model)        {            string result = await this.PutAsyncAsString(url, model);            return this.ConvertToModel<T>(result);        }        public async Task<string> PutAsyncAsString(string url, object model)        {
            // get response
            HttpResponseMessage responseMessage = await this._httpClient.PutAsync(url, this.GetStringContent(model));

            // check if response exists
            if (responseMessage != null)            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);            }            throw new HttpRequestException("Http Response message null");        }        public async Task<T> DeleteAsync<T>(string url)        {            string result = await this.DeleteAsyncAsString(url);            return this.ConvertToModel<T>(result);        }        public async Task<string> DeleteAsyncAsString(string url)        {
            // get response
            HttpResponseMessage responseMessage = await this._httpClient.DeleteAsync(url);

            // check if response exists
            if (responseMessage != null)            {
                // get converted response
                return await this.GetResponseAsString(responseMessage);            }            throw new HttpRequestException("Http Response message null");        }






        #endregion
        #region Utility
        protected virtual async Task<string> GetResponseAsString(HttpResponseMessage responseMessage)        {            string result = await responseMessage.Content.ReadAsStringAsync();

            // check if null
            if (!String.IsNullOrEmpty(result))            {
                // check if success
                if (responseMessage.IsSuccessStatusCode)                {                    return result;                }            }            throw new Exception(result);        }        protected T ConvertToModel<T>(string content)        {
            //convert to model
            return JsonConvert.DeserializeObject<T>(content);        }        protected StringContent GetStringContent(object model)        {
            // convert model to json content
            string json = JsonConvert.SerializeObject(model);            return new StringContent(json, Encoding.UTF8, "application/json");        }        #endregion
    }
}
