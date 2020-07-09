using System;
using System.Threading.Tasks;
using HubSpot.Sync.Service.HttpClientData;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Service
{
    public class DealService : IDealService
    {
        private readonly IHubspotHttpClientService httpclient;
        private readonly IToHubspotHttpClientService toHubspotHttpClientService;
        public DealService(IHubspotHttpClientService httpclient, IToHubspotHttpClientService toHubspotHttpClientService)        {            this.httpclient = httpclient;            this.toHubspotHttpClientService = toHubspotHttpClientService;        }

        public async Task<V3PropertyResult> GetAllProperties()
        {
            return await this.httpclient.GetAsync<V3PropertyResult>("/crm/v3/properties/deals");
        }

        public async Task<V3ResultModel> GetAllDeals(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/deals?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }
            else
            {
                url = "/crm/v3/objects/deals?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.httpclient.GetAsync<V3ResultModel>(url);
        }


        public async Task<object> BatchCreateDeal(HubspotBatchCreation deal)        {            return await this.toHubspotHttpClientService.PostAsyncWithError<object>("/crm/v3/objects/deals/batch/create", deal);        }

        public async Task<V3ResultModel> GetAllExistingDeals(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/deals?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }
            else
            {
                url = "/crm/v3/objects/deals?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.toHubspotHttpClientService.GetAsync<V3ResultModel>(url);
        }


        public async Task<AssociationBatchResponse> CreateDealAssociationBatch(AssociationBatchCreation createBatchDeal)        {            return await this.toHubspotHttpClientService.PostAsync<AssociationBatchResponse>("/crm/v3/associations/company/deal/batch/create", createBatchDeal);        }


        public async Task<AssociationBatchResponse> CreateContactDealAssociationBatch(AssociationBatchCreation createBatchDeal)        {            return await this.toHubspotHttpClientService.PostAsync<AssociationBatchResponse>("/crm/v3/associations/deal/contact/batch/create", createBatchDeal);        }
    }
}
