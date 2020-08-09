using System;
using System.Threading.Tasks;

namespace HubSpot.Sync.API.Interface
{
    public interface ICompanyHubspotService
    {
        Task CreateCompanies();
        Task UpdateCompanyProperty();
    }
}
