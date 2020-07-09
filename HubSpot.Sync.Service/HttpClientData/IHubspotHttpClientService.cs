using System;
using System.Threading.Tasks;
using HubSpotTest.Model.HubSpot;

namespace HubSpot.Sync.Service.HttpClientData
{
    public interface IHubspotHttpClientService: IHttpClientService
    {
        Task<HubspotResponseModel<T>> GetAsyncWithError<T>(string url);        Task PutAsyncWithError(string url, object model);        Task<HubspotResponseModel<T>> PostAsyncWithError<T>(string url, object model);        Task<HubspotResponseModel<T>> PutAsyncWithError<T>(string url, object model);        Task<HubspotResponseModel<T>> DeleteAsyncWithError<T>(string url);        Task<string> PostAsyncAsUseDeveloperApiKeyAsString(string url, object model);
    }
}
