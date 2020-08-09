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
    public class AssociationHubspotService: HubspotBaseService, IAssociationHubspotService
    {

        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
        private readonly IDealService dealService;
        private readonly ILogger<AssociationHubspotService> logger;

        string companyproperties = null;
        string contactproperties = null;
        string dealproperties = null;
        public AssociationHubspotService(IOptions<SyncPropertyMapperSettings> syncPropertySettings,
            ICompanyService companyService,
            IContactService contactService,
            IDealService dealService,
            ILogger<AssociationHubspotService> logger): base(syncPropertySettings, companyService, contactService, dealService)
        {
            this.companyService = companyService;
            this.contactService = contactService;
            this.dealService = dealService;
            this.logger = logger;
        }

        public async Task CreateAssociationCompanies()
        {
            try
            {
                List<AssociationBatchCreationSub> batchContactCompanyAssociationList = new List<AssociationBatchCreationSub>();
                List<AssociationBatchCreationSub> batchCompanyDealAssociationList = new List<AssociationBatchCreationSub>();
                List<AssociationBatchCreationSub> batchContactDealAssociationList = new List<AssociationBatchCreationSub>();
                companyproperties = this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber;
                contactproperties = this.syncPropertySettings.Value.Contact.HubspotProperties.CustomerNumber + "," + this.syncPropertySettings.Value.Contact.HubspotProperties.Email;
                dealproperties = this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID + "," + this.syncPropertySettings.Value.Deal.HubspotProperties.Name;

                this.logger.LogInformation("AssociationHubspotService: Start to Get All Companies");
                var companyList = await GetAllCompanies(companyproperties + "&associations=contacts,deals");
                this.logger.LogInformation("AssociationHubspotService: End to Get All Companies :  Company List Count : " + companyList.Count);


                this.logger.LogInformation("AssociationHubspotService: Start to Get All Existing Companies");
                var existingCompanyList = await GetExistingCompanies(companyproperties + "&associations=contacts,deals");
                this.logger.LogInformation("AssociationHubspotService: End to Get All Existing Companies : Existing Company List Count : " + existingCompanyList.Count);

                //this.logger.LogInformation("AssociationHubspotService: Start to Get All Contacts");
                //var contactList = await GetAllContacts(contactproperties + "&associations=companies,deals");
                //this.logger.LogInformation("AssociationHubspotService: End to Get All Contacts :  Contact List Count : " + contactList.Count);


                //this.logger.LogInformation("AssociationHubspotService: Start to Get All Existing Contacts");
                //var existingcontactList = await GetExistingContacts(contactproperties + "&associations=companies,deals");
                //this.logger.LogInformation("AssociationHubspotService: End to Get All Existing Contacts : Existing Contact List Count : " + existingcontactList.Count);


                this.logger.LogInformation("AssociationHubspotService: Start to Get All Deals");
                var dealList = await GetAllDeals(dealproperties + "&associations=contacts,companies");
                this.logger.LogInformation("AssociationHubspotService: End to Get All Deals :  Deal List Count : " + dealList.Count);


                this.logger.LogInformation("AssociationHubspotService: Start to Get All Existing Deals");
                var existingDealList = await GetExistingDeals(dealproperties + "&associations=contacts,companies");
                this.logger.LogInformation("AssociationHubspotService: End to Get All Existing Deals : Existing Deal List Count : " + existingDealList.Count);



                foreach (var company in companyList)
                {
                    if (company != null)
                    {
                        var companyInfo = this.GetCompanyInformation(existingCompanyList, company);
                        if (companyInfo != null)
                        {
                            //if (company.associations != null && company.associations.contacts != null && company.associations.contacts.results.Count > 0)
                            //{

                            //    foreach (var item in company.associations.contacts.results)
                            //    {
                            //        var contact = this.GetContactInformation(existingcontactList, contactList, long.Parse(item.id));
                            //        if (contact != null)
                            //        {
                            //            if (contact.associations != null && contact.associations.companies != null && contact.associations.companies.results.Count > 0)
                            //            {
                            //                var result = contact.associations.companies.results.FirstOrDefault(x => x.id == companyInfo.id);
                            //                if (result == null)
                            //                {
                            //                    var batchAssociate = this.GenerateAssociationProperty(contact.idLong, companyInfo.idLong, item.type);
                            //                    batchContactCompanyAssociationList.Add(batchAssociate);
                            //                }

                            //            }
                            //            else
                            //            {
                            //                var batchAssociate = this.GenerateAssociationProperty(contact.idLong, companyInfo.idLong, item.type);
                            //                batchContactCompanyAssociationList.Add(batchAssociate);

                            //            }


                            //        }

                            //    }
                            //}


                            // Deal Assciatiion with Company
                            if (company.associations != null && company.associations.deals != null && company.associations.deals.results.Count > 0)
                            {
                               // var extistingnewCompany = existingDealList.Where(x => (x.associations!=null && x.associations.companies!=null && x.associations.companies.results.Count > 0)).ToList();

                                foreach (var item in company.associations.deals.results)
                                {
                                    var deal = this.GetDealInformation(existingDealList, dealList, long.Parse(item.id));
                                    if (deal != null)
                                    {
                                        if (deal.associations != null && deal.associations.companies != null && deal.associations.companies.results.Count > 0)
                                        {
                                            var result = deal.associations.companies.results.FirstOrDefault(x => x.id == companyInfo.id);
                                            if (result == null)
                                            {
                                                var batchAssociate = this.GenerateAssociationProperty(deal.idLong, companyInfo.idLong, item.type);
                                                batchCompanyDealAssociationList.Add(batchAssociate);
                                            }

                                        }
                                        else
                                        {
                                            var batchAssociate = this.GenerateAssociationProperty(deal.idLong, companyInfo.idLong, item.type);
                                            batchCompanyDealAssociationList.Add(batchAssociate);

                                        }
                                    }

                                }

                            }

                        }

                    }

                }


                // Deal Association Connection
                //foreach (var deal in dealList)
                //{
                //    if (deal != null)
                //    {
                //        if (deal.associations != null && deal.associations.contacts != null)
                //        {
                //            var dealInfo = this.GetDealDataByExisting(existingDealList, deal);
                //            if (dealInfo != null)
                //            {
                //                foreach (var item in deal.associations.contacts.results)
                //                {
                //                    var contact = this.GetContactInformationByDeal(existingcontactList, contactList, long.Parse(item.id));
                //                    if (contact != null)
                //                    {
                //                        if (contact.associations != null && contact.associations.deals != null && contact.associations.deals.results.Count > 0)
                //                        {
                //                            var result = contact.associations.deals.results.FirstOrDefault(x => x.id == dealInfo.id);
                //                            if (result == null)
                //                            {
                //                                var batchAssociate = this.GenerateAssociationProperty(dealInfo.idLong, contact.idLong, item.type);
                //                                batchContactDealAssociationList.Add(batchAssociate);
                //                            }

                //                        }
                //                        else
                //                        {
                //                            var batchAssociate = this.GenerateAssociationProperty(contact.idLong, dealInfo.idLong, item.type);
                //                            batchContactDealAssociationList.Add(batchAssociate);

                //                        }


                //                    }

                //                }
                //            }
                //        }
                //    }
                //}




                if (batchContactCompanyAssociationList.Count > 0)
                {
                    int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                    do
                    {
                        try
                        {
                            this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Company Association full count : " + batchContactCompanyAssociationList.Count);

                            endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                            if (batchContactCompanyAssociationList.Count <= endCount)
                            {
                                endCount = batchContactCompanyAssociationList.Count;
                            }

                            // pick data but batch creation List
                            List<AssociationBatchCreationSub> batchAssociation = batchContactCompanyAssociationList.Take(endCount).ToList();

                            var dealCreation = new AssociationBatchCreation();
                            dealCreation.inputs = batchAssociation;
                            //batch and create data
                            var response = await this.companyService.CreateCompanyAssociationBatch(dealCreation);

                            // if there is response remove from the original List
                            if (response != null)
                            {
                                var result = JsonConvert.DeserializeObject<AsscoaitionResponse>(JsonConvert.SerializeObject(response));
                                if (result.status != "COMPLETE")
                                {
                                    this.logger.LogError("Log Error");
                                }
                                batchContactCompanyAssociationList.RemoveRange(0, endCount);
                            }

                            this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Company Association Create endCount : " + endCount);
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogError("AssociationHubspotService:CreateAssociationCompanies:CreateDealAssociation:Batch Company Association Exception for endCount : " + endCount);
                            this.logger.LogError(ex.Message);
                            this.logger.LogError(ex, ex.Message);
                        }

                    } while (batchContactCompanyAssociationList.Count > 0);
                }




                    if (batchCompanyDealAssociationList.Count > 0)
                    {
                        int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                        do
                        {
                            try
                            {
                                this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Deal Association full count : " + batchCompanyDealAssociationList.Count);

                                endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                                if (batchCompanyDealAssociationList.Count <= endCount)
                                {
                                    endCount = batchCompanyDealAssociationList.Count;
                                }

                                // pick data but batch creation List
                                List<AssociationBatchCreationSub> batchAssociation = batchCompanyDealAssociationList.Take(endCount).ToList();

                                var dealCreation = new AssociationBatchCreation();
                                dealCreation.inputs = batchAssociation;
                                //batch and create data
                                var response = await this.dealService.CreateDealAssociationBatch(dealCreation);

                                // if there is response remove from the original List
                                if (response != null)
                                {
                                    var result = JsonConvert.DeserializeObject<AsscoaitionResponse>(JsonConvert.SerializeObject(response));
                                    if (result.status !="COMPLETE")
                                    {
                                        this.logger.LogError("Log Error");
                                    }
                                    batchCompanyDealAssociationList.RemoveRange(0, endCount);
                                }

                                this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Deal Association Create endCount : " + endCount);
                            }
                            catch (Exception ex)
                            {
                                this.logger.LogError("AssociationHubspotService:CreateAssociationCompanies:CreateDealAssociation:Batch Deal Association Exception for endCount : " + endCount);
                                this.logger.LogError(ex.Message);
                                this.logger.LogError(ex, ex.Message);
                            }

                        } while (batchCompanyDealAssociationList.Count > 0);
                    }


                            // Contact to Deal

                            if (batchContactDealAssociationList.Count > 0)
                            {
                                int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                                do
                                {
                                    try
                                    {
                                        this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Deal Association full count : " + batchContactDealAssociationList.Count);

                                        endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;

                                        if (batchContactDealAssociationList.Count <= endCount)
                                        {
                                            endCount = batchContactDealAssociationList.Count;
                                        }

                                        // pick data but batch creation List
                                        List<AssociationBatchCreationSub> batchAssociation = batchContactDealAssociationList.Take(endCount).ToList();

                                        var dealCreation = new AssociationBatchCreation();
                                        dealCreation.inputs = batchAssociation;
                                        //batch and create data
                                        var response = await this.dealService.CreateContactDealAssociationBatch(dealCreation);

                                        // if there is response remove from the original List
                                        if (response != null)
                                        {
                                                var result = JsonConvert.DeserializeObject<AsscoaitionResponse>(JsonConvert.SerializeObject(response));
                                                if (result.status != "COMPLETE")
                                                {
                                                    this.logger.LogError("Log Error");
                                                }

                                                batchContactDealAssociationList.RemoveRange(0, endCount);
                                        }

                                        this.logger.LogInformation("AssociationHubspotService:CreateAssociationCompanies: Batch Deal Association Create endCount : " + endCount);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.logger.LogError("AssociationHubspotService:CreateAssociationCompanies:CreateDealAssociation:Batch Deal Association Exception for endCount : " + endCount);
                                        this.logger.LogError(ex.Message);
                                        this.logger.LogError(ex, ex.Message);
                                    }

                                } while (batchContactDealAssociationList.Count > 0);
                            }


            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }


        }


     



    }
}
