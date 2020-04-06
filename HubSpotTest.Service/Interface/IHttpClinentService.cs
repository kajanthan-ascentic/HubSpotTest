using System;
using System.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace HubSpotTest.Service.Interface
{
    public interface IHttpClinentService
    {
        Task<string> GetAsync(string uri);
        Task<string> PostAsync(string uri, string data);
    }
}
