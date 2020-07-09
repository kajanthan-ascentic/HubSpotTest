using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.Sync.API.Interface;
using HubSpot.Sync.API.Model.Settings;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HubSpot.Sync.API.Service
{
    public class CompanyHubspotService: HubspotBaseService, ICompanyHubspotService
    {
        private readonly ICompanyService companyService;
        private readonly ILogger<CompanyHubspotService> logger;

        string properties = null;
        public CompanyHubspotService(IOptions<SyncPropertyMapperSettings> syncPropertySettings, ICompanyService companyService,
            ILogger<CompanyHubspotService> logger, IDealService dealService, IContactService contactService) :
            base(syncPropertySettings, companyService, contactService, dealService)
        {
            this.companyService = companyService;
            this.logger = logger;
        }

        public async Task CreateCompanies()
        {
            this.logger.LogInformation("************* START TO CREATE COMPANIES ***************************");
            List<HubspotBatchCreateProperty> batchCompanyList = new List<HubspotBatchCreateProperty>();

            this.logger.LogInformation("CompanyHubspotService: Start to Get All Properties");
            var propertyList = await companyService.GetAllProperties();
            this.logger.LogInformation("CompanyHubspotService: End to Get All Properties " + propertyList.results.Count);

            if(propertyList.results !=null && propertyList.results.Count > 0)
            {
                properties = String.Join(",", propertyList.results.Where(x=>x.modificationMetadata.readOnlyValue == false).Select(p => p.name));
            }

            this.logger.LogInformation("Properties " + properties);


            this.logger.LogInformation("CompanyHubspotService: Start to Get All Companies");
            var companyList = await GetAllCompanies(properties);
            this.logger.LogInformation("CompanyHubspotService: End to Get All Companies :  Company List Count : " + companyList.Count);


            this.logger.LogInformation("CompanyHubspotService: Start to Get All Existing Companies");
            var existingCompanyList = await GetExistingCompanies(properties);

            this.logger.LogInformation("CompanyHubspotService: End to Get All Existing Companies : Existing Company List Count : " + existingCompanyList.Count);

            foreach (var company in companyList)
            {
                var customerId = company.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber);
                this.logger.LogInformation("CompanyHubspotService: Customer Id: " + customerId);

                Result companyExistByCustomerId = existingCompanyList.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber, out object value) &&
                                                                                 value.ToString() == customerId.ToString()).FirstOrDefault();
                if (companyExistByCustomerId == null)
                {
                    var batchProperty = new HubspotBatchCreateProperty();
                    company.Properties.Remove("createdate");
                    company.Properties.Remove("hs_lastmodifieddate");
                    company.Properties.Remove("hs_object_id");
                    batchProperty.Properties = (from kv in company.Properties
                                                where kv.Value != null
                                                select kv).ToDictionary(kv => kv.Key, kv => kv.Value); 

                    batchCompanyList.Add(batchProperty);
                }
                else
                {
                    this.logger.LogInformation("CompanyHubspotService: Customer Id: Already Exist in the Company" + customerId);
                }

            }



            this.logger.LogInformation("CompanyHubspotService: Total Batch Create Company Count " + batchCompanyList.Count);

            if (batchCompanyList.Count > 0)
            {                int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                do                {                    try                    {
                        this.logger.LogInformation("CompanyHubspotService:CreateCompanies: Batch Company full count : " + batchCompanyList.Count);                        endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                        if (batchCompanyList.Count <= endCount)                        {                            endCount = batchCompanyList.Count;                        }

                        // pick data but batch creation List
                        List<HubspotBatchCreateProperty> batchCompany = batchCompanyList.Take(endCount).ToList();

                        var companyCreation = new HubspotBatchCreation();                        companyCreation.inputs = batchCompany;

                        //batch and create data
                        var response = await this.companyService.BatchCreateCompany(companyCreation);

                        // if there is response remove from the original List
                        if (response != null)                        {                            batchCompanyList.RemoveRange(0, endCount);                        }

                        this.logger.LogInformation("CompanyHubspotService:CreateCompanies: Batch Company Create endCount : " + endCount);                    }                    catch (Exception ex)                    {
                        this.logger.LogError("CompanyHubspotService:CreateCompanies :batchCreate Exception for endCount : " + endCount);
                        this.logger.LogError(ex.Message);
                        this.logger.LogError(ex, ex.Message);                    }                } while (batchCompanyList.Count > 0);            }


            this.logger.LogInformation("************* END TO CREATE COMPANIES ***************************");
        }

    }
}
