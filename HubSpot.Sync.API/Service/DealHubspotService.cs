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
using Newtonsoft.Json;

namespace HubSpot.Sync.API.Service
{
    public class DealHubspotService: HubspotBaseService, IDealHubspotService
    {

        private readonly ILogger<DealHubspotService> logger;
        private readonly IDealService dealService;
        string properties = null;
        public DealHubspotService(IOptions<SyncPropertyMapperSettings> syncPropertySettings, IDealService dealService,
            ILogger<DealHubspotService> logger, IContactService contactService, ICompanyService companyService) : base(syncPropertySettings, companyService, contactService, dealService)
        {
            this.dealService = dealService;
            this.logger = logger;
        }



        public async Task CreateDeals()
        {
            this.logger.LogInformation("************* START TO CREATE DEALS ***************************");
            List<HubspotBatchCreateProperty> batchDealList = new List<HubspotBatchCreateProperty>();

            this.logger.LogInformation("DealHubspotService: Start to Get All Properties");
            var propertyList = await dealService.GetAllProperties();
            this.logger.LogInformation("DealHubspotService: End to Get All Properties " + propertyList.results.Count);

            if (propertyList.results != null && propertyList.results.Count > 0)
            {
                properties = String.Join(",", propertyList.results.Where(x => x.modificationMetadata.readOnlyValue == false).Select(p => p.name));

            }

            this.logger.LogInformation("Properties " + properties);

            this.logger.LogInformation("DealHubspotService: Start to Get All Deals");
            var dealList = await GetAllDeals(properties);
            this.logger.LogInformation("DealHubspotService: End to Get All Deals :  Deal List Count : " + dealList.Count);


            this.logger.LogInformation("DealHubspotService: Start to Get All Existing Deals");
            var existingDealList = await GetExistingDeals(properties);

            this.logger.LogInformation("DealHubspotService: End to Get All Existing Deals : Existing Deal List Count : " + existingDealList.Count);

            foreach (var deal in dealList)
            {
                var insuranceId = deal.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID);
                this.logger.LogInformation("DealHubspotService: Insurance Id: " + insuranceId);

                if (insuranceId != null)
                {

                    Result dealExistByInsuranceId = existingDealList.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID, out object value) &&
                                                                                     value.ToString() == insuranceId.ToString()).FirstOrDefault();
                    if (dealExistByInsuranceId == null)
                    {
                        var batchProperty = new HubspotBatchCreateProperty();
                        deal.Properties.Remove("createdate");
                        deal.Properties.Remove("hs_lastmodifieddate");
                        deal.Properties.Remove("hs_object_id");
                        deal.Properties.Remove("hubspot_owner_id");
                        batchProperty.Properties = (from kv in deal.Properties
                                                    where kv.Value != null
                                                    select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                        batchDealList.Add(batchProperty);
                    }
                    else
                    {
                        this.logger.LogInformation("DealHubspotService: Insurance Id: Already Exist in the Deal" + insuranceId);
                    }

                }else
                {
                    this.logger.LogInformation("DealHubspotService: Insurance Id: NULL" + deal.id);
                    var batchProperty = new HubspotBatchCreateProperty();
                    deal.Properties.Remove("createdate");
                    deal.Properties.Remove("hs_lastmodifieddate");
                    deal.Properties.Remove("hs_object_id");
                    deal.Properties.Remove("hubspot_owner_id");
                    batchProperty.Properties = (from kv in deal.Properties
                                                where kv.Value != null
                                                select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                    batchDealList.Add(batchProperty);
                }
            }



            this.logger.LogInformation("DealHubspotService: Total Batch Create Deal Count " + batchDealList.Count);

            if (batchDealList.Count > 0)
            {                int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                do                {                    try                    {
                        this.logger.LogInformation("DealHubspotService:CreateDeals: Batch Deal full count : " + batchDealList.Count);                        endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                        if (batchDealList.Count <= endCount)                        {                            endCount = batchDealList.Count;                        }

                        // pick data but batch creation List
                        List<HubspotBatchCreateProperty> batchDeal = batchDealList.Take(endCount).ToList();

                        var dealCreation = new HubspotBatchCreation();                        dealCreation.inputs = batchDeal;

                        //batch and create data
                        var response = await this.dealService.BatchCreateDeal(dealCreation);

                        // if there is response remove from the original List
                        if (response != null)                        {
                            var result = JsonConvert.DeserializeObject<V3ResponseResult>(JsonConvert.SerializeObject(response));                            if (!result.Status)
                            {
                                this.logger.LogError("Log Error");
                            }                            batchDealList.RemoveRange(0, endCount);                        }

                        this.logger.LogInformation("DealHubspotService:CreateDeals: Batch Deal Create endCount : " + endCount);                    }                    catch (Exception ex)                    {
                        this.logger.LogError("DealHubspotService:CreateDeals :batchCreate Exception for endCount : " + endCount);
                        this.logger.LogError(ex.Message);
                        this.logger.LogError(ex, ex.Message);                    }                } while (batchDealList.Count > 0);            }


            this.logger.LogInformation("************* END TO CREATE DEALS ***************************");
        }

      
    }
}
