using System;
using System.Threading.Tasks;

namespace HubSpot.Sync.Service.HttpClientData
{
    public interface IHttpClientService
    {

        Task<T> GetAsync<T>(string url);        Task<string> GetAsyncAsString(string url);        Task<T> PostAsync<T>(string url, object model);        Task<string> PostAsyncAsString(string url, object model);        Task<T> PutAsync<T>(string url, object model);        Task<string> PutAsyncAsString(string url, object model);        Task<T> DeleteAsync<T>(string url);        Task<string> DeleteAsyncAsString(string url);
    }
}
