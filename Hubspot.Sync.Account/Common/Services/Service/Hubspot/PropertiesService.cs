using Hubspot.Sync.Account.Common.Services.Interface;
using Hubspot.Sync.Account.Common.Services.Interface.Hubspot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account.Common.Services.Service.Hubspot
{
    public class PropertiesService<T> : IPropertiesService<T>
    {
        private const string URL_FORMAT = "/crm/v3/properties/{0}";
        private readonly IHttpClientService<T> _httpClientService = null;
        private string _requestUrl = String.Empty;
        public PropertiesService(IHttpClientService<T> httpClientService, string objectType)
        {
            this._httpClientService = httpClientService;
            this._requestUrl = String.Format(URL_FORMAT, objectType);
        }

        public Task<T> GetAll()
        {
            return this._httpClientService.GetAsync(this._requestUrl);
        }

        public Task<T> GetByName()
        {
            throw new NotImplementedException();
        }

        public Task<T> Post(object model)
        {
            return this._httpClientService.PostAsync(this._requestUrl, model);
        }

        Task<IEnumerable<T>> IHubSpotService<T>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

