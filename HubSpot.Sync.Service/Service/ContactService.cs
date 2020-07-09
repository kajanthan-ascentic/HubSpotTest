using System;
using System.Dynamic;
using System.Threading.Tasks;
using HubSpot.Sync.Service.HttpClientData;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Service
{
    public class ContactService :  IContactService
    {
        private readonly IHubspotHttpClientService httpclient;
        private readonly IToHubspotHttpClientService toHubspotHttpClientService;
        public ContactService(IHubspotHttpClientService httpclient, IToHubspotHttpClientService toHubspotHttpClientService)        {            this.httpclient = httpclient;            this.toHubspotHttpClientService = toHubspotHttpClientService;        }

        public async Task<V3PropertyResult> GetAllProperties()
        {
            return await this.httpclient.GetAsync<V3PropertyResult>("/crm/v3/properties/contacts");
        }

        public async Task<V3ResultModel> GetAllContacts(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/contacts?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }
            else
            {
                url = "/crm/v3/objects/contacts?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.httpclient.GetAsync<V3ResultModel>(url);
        }

        public async Task<object> BatchCreateContact(HubspotBatchCreation contacts)        {            return await this.toHubspotHttpClientService.PostAsyncWithError<object>("/crm/v3/objects/contacts/batch/create", contacts);        }

        public async Task<V3ResultModel> GetAllExistingContacts(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/contacts?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }
            else
            {
                url = "/crm/v3/objects/contacts?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.toHubspotHttpClientService.GetAsync<V3ResultModel>(url);
        }
    }
}
