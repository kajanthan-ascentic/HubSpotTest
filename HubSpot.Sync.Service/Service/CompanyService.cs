using System;
using System.Threading.Tasks;
using HubSpot.Sync.Service.HttpClientData;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Service
{
    public class CompanyService: ICompanyService
    {
        private readonly IHubspotHttpClientService httpclient;
        private readonly IToHubspotHttpClientService toHubspotHttpClientService;

        public CompanyService(IHubspotHttpClientService httpclient, IToHubspotHttpClientService toHubspotHttpClientService)        {            this.httpclient = httpclient;            this.toHubspotHttpClientService = toHubspotHttpClientService;        }

        public async Task<V3PropertyResult> GetAllProperties()
        {
            return await this.httpclient.GetAsync<V3PropertyResult>("/crm/v3/properties/companies");
        }

        public async Task<V3ResultModel> GetAllCompanies(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/companies?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }else
            {
                url = "/crm/v3/objects/companies?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.httpclient.GetAsync<V3ResultModel>(url);
        }

        public async Task<object> BatchCreateCompany(HubspotBatchCreation company)        {            return await this.toHubspotHttpClientService.PostAsyncWithError<object>("/crm/v3/objects/companies/batch/create", company);        }


        public async Task<V3ResultModel> GetAllExistingCompanies(int limit, string pageno, string properties)
        {
            string url = null;
            if (properties != null)
            {
                url = "/crm/v3/objects/companies?limit=" + limit + "&properties=" + properties + "&after=" + pageno + "&archived=false";
            }
            else
            {
                url = "/crm/v3/objects/companies?limit=" + limit + "&after=" + pageno + "&archived=false";
            }
            return await this.toHubspotHttpClientService.GetAsync<V3ResultModel>(url);
        }

        public async Task<AssociationBatchResponse> CreateCompanyAssociationBatch(AssociationBatchCreation createBatchDeal)        {            return await this.toHubspotHttpClientService.PostAsync<AssociationBatchResponse>("/crm/v3/associations/company/contact/batch/create", createBatchDeal);        }
    }
}
