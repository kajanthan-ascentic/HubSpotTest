using System;
using System.Threading.Tasks;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Interface
{
    public interface ICompanyService
    {
        Task<V3ResultModel> GetAllCompanies(int limit, string pageno, string properties);
        Task<object> BatchCreateCompany(HubspotBatchCreation company);

        Task<V3ResultModel> GetAllExistingCompanies(int limit, string pageno, string properties);

        Task<V3PropertyResult> GetAllProperties();

        Task<AssociationBatchResponse> CreateCompanyAssociationBatch(AssociationBatchCreation createBatchDeal);

        Task<object> BatchUpdateCompany(HubspotBatchUpdate company);
    }
}
