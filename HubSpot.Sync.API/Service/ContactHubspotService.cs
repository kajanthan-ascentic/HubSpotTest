using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.Sync.API.Interface;
using HubSpot.Sync.API.Model;
using HubSpot.Sync.API.Model.Settings;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HubSpot.Sync.API.Service
{
    public class ContactHubspotService: HubspotBaseService, IContactHubspotService
    {
        private readonly IContactService contactService;
        private readonly ILogger<ContactHubspotService> logger;
        string properties = null;
        public ContactHubspotService(IOptions<SyncPropertyMapperSettings> syncPropertySettings, IContactService contactService,
            ILogger<ContactHubspotService> logger, ICompanyService companyService, IDealService dealService): base(syncPropertySettings, companyService, contactService, dealService)
        {
            this.contactService = contactService;
            this.logger = logger;
        }

        public async Task CreateContacts()
        {
            this.logger.LogInformation("************* START TO CREATE CONTACTS ***************************");
            List<HubspotBatchCreateProperty> batchContactList = new List<HubspotBatchCreateProperty>();

            this.logger.LogInformation("ContactHubspotService: Start to Get All Properties");
            var propertyList = await contactService.GetAllProperties();
            this.logger.LogInformation("ContactHubspotService: End to Get All Properties " + propertyList.results.Count);

            if (propertyList.results != null && propertyList.results.Count > 0)
            {
                properties = String.Join(",", propertyList.results.Where(x => x.modificationMetadata.readOnlyValue == false).Select(p => p.name));

            }

            this.logger.LogInformation("Properties " + properties);


            this.logger.LogInformation("ContactHubspotService: Start to Get All Contacts");
            var contactList = await GetAllContacts(properties);
            this.logger.LogInformation("ContactHubspotService: End to Get All Contacts :  Contact List Count : " + contactList.Count);


            this.logger.LogInformation("ContactHubspotService: Start to Get All Existing Contacts");
            var existingcontactList = await GetExistingContacts(properties);

            this.logger.LogInformation("ContactHubspotService: End to Get All Existing Contacts : Existing Contact List Count : " + existingcontactList.Count);

            foreach (var contact in contactList)
            {
                try
                {
                    var emailValue = contact.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Contact.HubspotProperties.Email);
                    this.logger.LogInformation("ContactHubspotService: Email ID: " + emailValue);
                    if (emailValue != null)
                    {

                        Result contactExistByEmail = existingcontactList.Where(x => x.Properties != null && x.Properties.TryGetValue(this.syncPropertySettings.Value.Contact.HubspotProperties.Email, out object value) &&
                                                                                         (value !=null && value.ToString() == emailValue.ToString())).FirstOrDefault();
                        if (contactExistByEmail == null)
                        {
                            var batchProperty = new HubspotBatchCreateProperty();
                            contact.Properties.Remove("createdate");
                            contact.Properties.Remove("lastmodifieddate");
                            contact.Properties.Remove("hs_object_id");
                            contact.Properties.Remove("associatedcompanyid");
                            contact.Properties.Remove("hubspot_owner_id");
                            batchProperty.Properties = (from kv in contact.Properties
                                                        where kv.Value != null
                                                        select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                            batchContactList.Add(batchProperty);
                        }
                        else
                        {
                            this.logger.LogInformation("ContactHubspotService: Email ID: Already Exist in the Contact" + emailValue);
                        }
                    }else
                    {
                        this.logger.LogInformation("ContactHubspotService: Email ID: Null -- No Email Found" + contact.id);

                        var batchProperty = new HubspotBatchCreateProperty();
                        contact.Properties.Remove("createdate");
                        contact.Properties.Remove("lastmodifieddate");
                        contact.Properties.Remove("hs_object_id");
                        contact.Properties.Remove("associatedcompanyid");
                        contact.Properties.Remove("hubspot_owner_id");
                        batchProperty.Properties = (from kv in contact.Properties
                                                    where kv.Value != null
                                                    select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                        batchContactList.Add(batchProperty);
                    }

                }
                catch (Exception ex)
                {
                    this.logger.LogInformation(ex.Message);
                    this.logger.LogInformation(JsonConvert.SerializeObject(ex));
                    this.logger.LogError(ex, ex.Message);
                }

            }



            this.logger.LogInformation("ContactHubspotService: Total Batch Create Contact Count " + batchContactList.Count);

            if (batchContactList.Count > 0)
            {                int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                do                {                    try                    {                       this.logger.LogInformation("ContactHubspotService:CreateContacts: Batch Contact full count : " + batchContactList.Count);                        endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                        if (batchContactList.Count <= endCount)                        {                            endCount = batchContactList.Count;                        }

                        // pick data but batch creation List
                        List<HubspotBatchCreateProperty> batchContact = batchContactList.Take(endCount).ToList();

                        var contactCreation = new HubspotBatchCreation();                        contactCreation.inputs = batchContact;

                        //batch and create data
                        var response = await this.contactService.BatchCreateContact(contactCreation);

                        // if there is response remove from the original List
                        if (response != null)                        {                            var result = JsonConvert.DeserializeObject<V3ResponseResult>(JsonConvert.SerializeObject(response));                            if(!result.Status)
                            {
                                this.logger.LogError("Log Error");
                            }                            batchContactList.RemoveRange(0, endCount);                        }                       this.logger.LogInformation("ContactHubspotService:CreateContacts: Batch Contact Create endCount : " + endCount);                    }                    catch (Exception ex)                    {                        this.logger.LogError("ContactHubspotService:CreateContacts :batchCreate Exception for endCount : " + endCount);                        this.logger.LogError(ex.Message);
                        this.logger.LogInformation(JsonConvert.SerializeObject(ex));
                        this.logger.LogError(ex, ex.Message);                    }                } while (batchContactList.Count > 0);            }


            this.logger.LogInformation("************* END TO CREATE CONTACTS ***************************");
        }



       


    }
}
