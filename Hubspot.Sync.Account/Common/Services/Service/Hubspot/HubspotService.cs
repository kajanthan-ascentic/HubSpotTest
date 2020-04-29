using Hubspot.Sync.Account.Common.Services.Interface;
using Hubspot.Sync.Account.Common.Services.Interface.Hubspot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hubspot.Sync.Account.Common.Services.Service.Hubspot
{
    public class HubspotService<T> : IHubSpotService<T>
    {
        private readonly IHttpClientService<T> _httpClientService = null;
        private string _requestUrl = String.Empty;

        public HubspotService(IHttpClientService<T> httpClientService, string objectType, string urlFormat)
        {
            this._httpClientService = httpClientService;
            this._requestUrl = String.Format(urlFormat, objectType);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return this._httpClientService.GetAllAsync(this._requestUrl);
        }

        public Task<T> GetByName()
        {
            throw new NotImplementedException();
        }

        public Task<T> Post(object model)
        {
            return this._httpClientService.PostAsync(this._requestUrl, model);
        }
    }
}
