using System;
using System.Dynamic;
using System.Threading.Tasks;
using HubSpotTest.Model.V3;

namespace HubSpot.Sync.Service.Interface
{
    public interface IContactService
    {
        Task<V3ResultModel> GetAllContacts(int limit, string pageno, string properties);

        Task<V3ResultModel> GetAllExistingContacts(int limit, string pageno, string properties);

        Task<object> BatchCreateContact(HubspotBatchCreation contacts);

        Task<V3PropertyResult> GetAllProperties();
    }
}
