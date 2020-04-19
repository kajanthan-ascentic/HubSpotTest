using HubSpotTest.Model.Awis.Webhook;
using HubSpotTest.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HubSpotTest.Service.Service
{
    public class AwisWebhook : IAwisWebhook
    {
        public AwisWebhook() 
        {

        }

        public Task AddCompanyToQueue(CompanyModel companyModel)
        {
            throw new NotImplementedException();
        }
    }
}
