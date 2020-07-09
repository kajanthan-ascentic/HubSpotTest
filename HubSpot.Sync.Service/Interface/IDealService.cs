using System;
using System.Threading.Tasks;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Interface
{
    public interface IDealService
    {
        Task<V3ResultModel> GetAllDeals(int limit, string pageno, string properties);

        Task<object> BatchCreateDeal(HubspotBatchCreation deal);

        Task<V3ResultModel> GetAllExistingDeals(int limit, string pageno, string properties);

        Task<V3PropertyResult> GetAllProperties();

        Task<AssociationBatchResponse> CreateDealAssociationBatch(AssociationBatchCreation createBatchDeal);

        Task<AssociationBatchResponse> CreateContactDealAssociationBatch(AssociationBatchCreation createBatchDeal);
    }
}
