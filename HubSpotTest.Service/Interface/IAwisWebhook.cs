using HubSpotTest.Model.Awis.Webhook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotTest.Service.Interface
{
    public interface IAwisWebhook
    {
        Task AddCompanyToQueue(CompanyModel companyModel);
    }
}
