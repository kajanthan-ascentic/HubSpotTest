using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HubSpotTest.Model;
using HubSpotTest.Model.HubSpot;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HubSpot.Sync.Service.HttpClientData
{
    public class ToHubspotHttpClientService: IToHubspotHttpClientService
    {
        private readonly IOptions<ToHubSpotSettings> hubspotSettings;        private readonly IHttpClientService httpClientService;
        private readonly HttpClient _httpClient = null;        public ToHubspotHttpClientService(IOptions<ToHubSpotSettings> hubspotSettings, IHttpClientService httpClientService)        {            this.hubspotSettings = hubspotSettings;
            this._httpClient = new HttpClient();            this.httpClientService = httpClientService;        }


        #region Requests
        public new async Task<T> GetAsync<T>(string url)        {            try            {                return await httpClientService.GetAsync<T>(this.GetRequestUrl(url));            }            catch (TooManyRequestException ex)
            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.GetAsync<T>(this.GetRequestUrl(url));            }        }


















        /// <summary>        /// Post and gets the response with the hubspot method        /// </summary>        /// <typeparam name="T"></typeparam>        /// <param name="url"></param>        /// <param name="model"></param>        /// <returns></returns>        public async Task<HubspotResponseModel<T>> GetAsyncWithError<T>(string url)        {            try            {
                // get response
                HttpResponseMessage responseMessage = await this._httpClient.GetAsync(this.GetRequestUrl(url));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);

                // get response
                HttpResponseMessage responseMessage = await this._httpClient.GetAsync(this.GetRequestUrl(url));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            throw new HttpRequestException("Http Response message null");        }        public new async Task<string> GetAsyncAsString(string url)        {            try            {                return await httpClientService.GetAsyncAsString(this.GetRequestUrl(url));            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.GetAsyncAsString(this.GetRequestUrl(url));            }
        }        public new async Task<T> PostAsync<T>(string url, object model)        {            try            {                return await httpClientService.PostAsync<T>(this.GetRequestUrl(url), model);            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.PostAsync<T>(this.GetRequestUrl(url), model);            }        }        public new async Task<string> PostAsyncAsString(string url, object model)        {            try            {                return await httpClientService.PostAsyncAsString(this.GetRequestUrl(url), model);            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.PostAsyncAsString(this.GetRequestUrl(url), model);            }        }        public new async Task<string> PostAsyncAsUseDeveloperApiKeyAsString(string url, object model)        {            try            {                return await httpClientService.PostAsyncAsString(this.GetRequestUrlUseDeveloper(url), model);            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.PostAsyncAsString(this.GetRequestUrlUseDeveloper(url), model);            }        }        public new async Task<T> PutAsync<T>(string url, object model)        {            try            {                return await httpClientService.PutAsync<T>(this.GetRequestUrl(url), model);            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.PutAsync<T>(this.GetRequestUrl(url), model);            }        }        public new async Task<string> PutAsyncAsString(string url, object model)        {            try            {                return await httpClientService.PutAsyncAsString(this.GetRequestUrl(url), model);            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.PutAsyncAsString(this.GetRequestUrl(url), model);            }        }        public new async Task<T> DeleteAsync<T>(string url)        {            try            {                return await httpClientService.DeleteAsync<T>(this.GetRequestUrl(url));            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.DeleteAsync<T>(this.GetRequestUrl(url));            }        }        public new async Task<string> DeleteAsyncAsString(string url)        {            try            {                return await httpClientService.DeleteAsyncAsString(this.GetRequestUrl(url));            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                return await httpClientService.DeleteAsyncAsString(this.GetRequestUrl(url));            }        }















        /// <summary>        /// Post and gets the response with the hubspot method        /// </summary>        /// <typeparam name="T"></typeparam>        /// <param name="url"></param>        /// <param name="model"></param>        /// <returns></returns>        public async Task<HubspotResponseModel<T>> PostAsyncWithError<T>(string url, object model)        {            try            {
                // get response
                HttpResponseMessage responseMessage = await this._httpClient.PostAsync(this.GetRequestUrl(url), this.GetStringContent(model));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);

                // get response
                HttpResponseMessage responseMessage = await this._httpClient.PostAsync(this.GetRequestUrl(url), this.GetStringContent(model));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            throw new HttpRequestException("Http Response message null");        }        public async Task PutAsyncWithError(string url, object model)        {            try            {                await this._httpClient.PutAsync(this.GetRequestUrl(url), this.GetStringContent(model));            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);                await this._httpClient.PutAsync(this.GetRequestUrl(url), this.GetStringContent(model));            }        }
















        /// <summary>        /// Post and gets the response with the hubspot method        /// </summary>        /// <typeparam name="T"></typeparam>        /// <param name="url"></param>        /// <param name="model"></param>        /// <returns></returns>        public async Task<HubspotResponseModel<T>> PutAsyncWithError<T>(string url, object model)        {            try            {
                // get response
                HttpResponseMessage responseMessage = await this._httpClient.PutAsync(this.GetRequestUrl(url), this.GetStringContent(model));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);

                // get response
                HttpResponseMessage responseMessage = await this._httpClient.PutAsync(this.GetRequestUrl(url), this.GetStringContent(model));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            throw new HttpRequestException("Http Response message null");        }        public async Task<HubspotResponseModel<T>> DeleteAsyncWithError<T>(string url)        {            try            {
                // get response
                HttpResponseMessage responseMessage = await this._httpClient.DeleteAsync(this.GetRequestUrl(url));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            catch (TooManyRequestException ex)            {                await Task.Delay(hubspotSettings.Value.RateLimitDelayMiliSeconds);

                // get response
                HttpResponseMessage responseMessage = await this._httpClient.DeleteAsync(this.GetRequestUrl(url));

                // check if response exists
                if (responseMessage != null)                {
                    // get converted response
                    return await this.GetHubSpotResponse<T>(responseMessage);                }            }            throw new HttpRequestException("Http Response message null");        }



        #endregion
        #region Helpers
        private string GetRequestUrl(string url)        {
            //append api key
            UriBuilder builder = new UriBuilder(this.hubspotSettings.Value.BaseAddress + url);            var query = HttpUtility.ParseQueryString(builder.Query);            query["hapikey"] = this.hubspotSettings.Value.ApiKey;            builder.Query = query.ToString();            return builder.ToString();        }        private string GetRequestUrlUseDeveloper(string url)        {
            //append api key
            UriBuilder builder = new UriBuilder(this.hubspotSettings.Value.BaseAddress + url);            var query = HttpUtility.ParseQueryString(builder.Query);            query["hapikey"] = this.hubspotSettings.Value.DeveloperApiKey;            builder.Query = query.ToString();            return builder.ToString();        }        private async Task<HubspotResponseModel<T>> GetHubSpotResponse<T>(HttpResponseMessage responseMessage)        {            string response = await responseMessage.Content.ReadAsStringAsync();            var result = new HubspotResponseModel<T>();

            // check if success , hubspots send 204 for update for older version api  (ex contact api still in v1)
            if (responseMessage.IsSuccessStatusCode ||
responseMessage.StatusCode == System.Net.HttpStatusCode.NoContent ||
responseMessage.StatusCode == System.Net.HttpStatusCode.TooManyRequests)            {                result.Status = true;                if (!String.IsNullOrEmpty(response))                {                    result.Data = this.ConvertToModel<T>(response);                }            }            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.TooManyRequests)            {                throw new TooManyRequestException("Hubspot Rate Limit Passed Exception");            }            else            {                result.Status = false;                if (!String.IsNullOrEmpty(response))                {                    result.ErrorData = this.ConvertToModel<HubspotErrorResponse>(response);                }            }            return result;        }


        #endregion
        #region Utility

        protected T ConvertToModel<T>(string content)        {
            //convert to model
            return JsonConvert.DeserializeObject<T>(content);        }        protected StringContent GetStringContent(object model)        {
            // convert model to json content
            string json = JsonConvert.SerializeObject(model);            return new StringContent(json, Encoding.UTF8, "application/json");        }


        #endregion


    }

}
