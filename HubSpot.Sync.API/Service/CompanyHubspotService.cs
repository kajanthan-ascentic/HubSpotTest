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
                if(customerId != null)
                {
                    Result companyExistByCustomerId = existingCompanyList.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber, out object value) &&
                                                                       value.ToString() == customerId.ToString()).FirstOrDefault();
                    if (companyExistByCustomerId == null)
                    {
                        var batchProperty = new HubspotBatchCreateProperty();
                        company.Properties.Remove("createdate");
                        company.Properties.Remove("hs_lastmodifieddate");
                        company.Properties.Remove("hs_object_id");
                        company.Properties.Remove("hubspot_owner_id");
                        batchProperty.Properties = (from kv in company.Properties
                                                    where kv.Value != null
                                                    select kv).ToDictionary(kv => kv.Key, kv => kv.Value);

                        batchCompanyList.Add(batchProperty);
                    }
                    else
                    {
                        this.logger.LogInformation("CompanyHubspotService: Customer Id: Already Exist in the Company" + customerId);
                    }

                }else
                {
                    this.logger.LogInformation("CompanyHubspotService: Customer Id: NULL : " + company.id);


                    var batchProperty = new HubspotBatchCreateProperty();
                    company.Properties.Remove("createdate");
                    company.Properties.Remove("hs_lastmodifieddate");
                    company.Properties.Remove("hs_object_id");
                    company.Properties.Remove("hubspot_owner_id");
                    batchProperty.Properties = (from kv in company.Properties
                                                where kv.Value != null
                                                select kv).ToDictionary(kv => kv.Key, kv => kv.Value);

                    batchCompanyList.Add(batchProperty);
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
                        if (response != null)                        {
                            var result = JsonConvert.DeserializeObject<V3ResponseResult>(JsonConvert.SerializeObject(response));                            if (!result.Status)
                            {
                                this.logger.LogError("Log Error");
                            }                            batchCompanyList.RemoveRange(0, endCount);                        }

                        this.logger.LogInformation("CompanyHubspotService:CreateCompanies: Batch Company Create endCount : " + endCount);                    }                    catch (Exception ex)                    {
                        this.logger.LogError("CompanyHubspotService:CreateCompanies :batchCreate Exception for endCount : " + endCount);
                        this.logger.LogError(ex.Message);
                        this.logger.LogError(ex, ex.Message);                    }                } while (batchCompanyList.Count > 0);            }


            this.logger.LogInformation("************* END TO CREATE COMPANIES ***************************");
        }


        public async Task UpdateCompanyProperty()
        {
            List<HubspotBatchUpdateProperty> batchCompanyList = new List<HubspotBatchUpdateProperty>();
            properties = "ac_sales_rep_initials, country, ac_country, ac_sales_rep";
            this.logger.LogInformation("CompanyHubspotService: Start to Get All Companies");
            var companyList = await GetAllCompanies(properties);
            this.logger.LogInformation("CompanyHubspotService: End to Get All Companies :  Company List Count : " + companyList.Count);
            var salesRepList = GetSalesRep();
            var countryList = GetAllCountries();

            foreach (var company in companyList)
            {
                var batchProperty = new HubspotBatchUpdateProperty();
                batchProperty.id = company.id;
                var existingSalesRep = company.Properties.FirstOrDefault(x => x.Key == "ac_sales_rep");
                if (existingSalesRep.Key == null && existingSalesRep.Value == null)
                {
                    var salesRep = company.Properties.FirstOrDefault(x => x.Key == "ac_sales_rep_initials");
                    batchProperty.Properties = new Dictionary<string, object>();
                    if (salesRep.Key != null && salesRep.Value != null)
                    {
                        this.logger.LogInformation("Sales Rep Name :" + salesRep.Value + " CompanyId :" + company.id);
                        var salesInternal = salesRepList.FirstOrDefault(x => x.Name.ToLower() == salesRep.Value.ToString().ToLower() || x.InternalValue.ToLower() == salesRep.Value.ToString().ToLower());
                        if (salesInternal != null)
                        {
                            batchProperty.Properties.Add("ac_sales_rep", salesInternal.InternalValue);
                        }

                    }
                    else
                    {
                        this.logger.LogInformation("Sales Rep not Available for Company Id:" + company.id);
                    }
                }else
                {
                    this.logger.LogInformation("Already Exist Sales Rep :" + company.id);
                }

                var existingCountry = company.Properties.FirstOrDefault(x => x.Key == "ac_country");

                if(existingCountry.Key == null && existingCountry.Value == null)
                {
                    var countryValue = company.Properties.FirstOrDefault(x => x.Key == "country");
                    if (countryValue.Key != null && countryValue.Value != null)
                    {
                        this.logger.LogInformation("Country Value :" + countryValue.Value + " CompanyId :" + company.id);
                        var countryIntenal = countryList.FirstOrDefault(x => x.Name.ToLower() == countryValue.Value.ToString().ToLower() || x.InternalValue.ToLower() == countryValue.Value.ToString().ToLower());
                        if (countryIntenal != null)
                        {
                            batchProperty.Properties.Add("ac_country", countryIntenal.InternalValue);
                        }
                    }
                    else
                    {
                        this.logger.LogInformation("Country Not Available for Company Id:" + company.id);
                    }

                }
                else
                {
                    this.logger.LogInformation("Already Exist Country :" + company.id);
                }




                if (batchProperty.Properties != null && batchProperty.Properties.Count > 0)
                {
                    batchCompanyList.Add(batchProperty);
                }

            }



            this.logger.LogInformation("CompanyHubspotService: Total Batch Update Company Count " + batchCompanyList.Count);


            if (batchCompanyList.Count > 0)
            {                int endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                do                {                    try                    {
                        this.logger.LogInformation("CompanyHubspotService:CreateCompanies: Batch Company full count : " + batchCompanyList.Count);                        endCount = this.syncPropertySettings.Value.HubspotBulkDealUploadLimit;                        if (batchCompanyList.Count <= endCount)                        {                            endCount = batchCompanyList.Count;                        }

                        logger.LogInformation("Daily Sync ExecuteAsync delay  begin");
                        await Task.Delay(this.syncPropertySettings.Value.BackgroundServiceInitialDelay);
                        logger.LogInformation("Daily Sync ExecuteAsync delay  end");

                        // pick data but batch creation List
                        List<HubspotBatchUpdateProperty> batchCompany = batchCompanyList.Take(endCount).ToList();

                        var companyUpdate = new HubspotBatchUpdate();                        companyUpdate.inputs = batchCompany;

                        //batch and create data
                        var response = await this.companyService.BatchUpdateCompany(companyUpdate);

                        // if there is response remove from the original List
                        if (response != null)                        {
                            var result = JsonConvert.DeserializeObject<V3ResponseResult>(JsonConvert.SerializeObject(response));                            if (!result.Status)
                            {
                                this.logger.LogError("Log Error");
                            }                            batchCompanyList.RemoveRange(0, endCount);                        }

                        this.logger.LogInformation("CompanyHubspotService:CreateCompanies: Batch Company Update endCount : " + endCount);                    }                    catch (Exception ex)                    {
                        this.logger.LogError("CompanyHubspotService:CreateCompanies :batchCreate Exception for endCount : " + endCount);
                        this.logger.LogError(ex.Message);
                        this.logger.LogError(ex, ex.Message);                    }                } while (batchCompanyList.Count > 0);            }




        }


        public List<PropertyInternalList> GetSalesRep()
        {
            List<PropertyInternalList> listItem = new List<PropertyInternalList>();

            var anbl = new PropertyInternalList()
            {
                Name = "Anders Blok",
                InternalValue = "ANBL"
            };

            var cha = new PropertyInternalList()
            {
                Name = "Christoffer Hallbäck",
                InternalValue = "CHA"
            };

            var cci = new PropertyInternalList()
            {
                Name = "Claudio Cianciolo",
                InternalValue = "CCI"
            };

            var fka = new PropertyInternalList()
            {
                Name = "Fredrik Karlström",
                InternalValue = "FKA"
            };

            var kso = new PropertyInternalList()
            {
                Name = "Kjersti Solend",
                InternalValue = "KSO"
            };

            var miwa = new PropertyInternalList()
            {
                Name = "Mikael Wacklin",
                InternalValue = "MIWA"
            };


            var pgu = new PropertyInternalList()
            {
                Name = "Peter Gustafsson",
                InternalValue = "PGU"
            };

            var rolu = new PropertyInternalList()
            {
                Name = "Robert Lundquist",
                InternalValue = "ROLU"
            };


            var rma = new PropertyInternalList()
            {
                Name = "Robert Martinsen",
                InternalValue = "RMA"
            };

            var ski = new PropertyInternalList()
            {
                Name = "Stephan Kinnmark",
                InternalValue = "SKI"
            };

            var uka = new PropertyInternalList()
            {
                Name = "Ulf Kardell",
                InternalValue = "UKA"
            };

            listItem.Add(anbl);
            listItem.Add(cha);
            listItem.Add(cci);
            listItem.Add(fka);
            listItem.Add(kso);
            listItem.Add(miwa);
            listItem.Add(pgu);
            listItem.Add(rolu);
            listItem.Add(rma);
            listItem.Add(ski);
            listItem.Add(uka);

            return listItem;
        }

        public List<PropertyInternalList> GetAllCountries()
        {
            List<PropertyInternalList> listItem = new List<PropertyInternalList>();

            var anbl = new PropertyInternalList()
            {
                Name = "UK",
                InternalValue = "EN"
            };

            var cha = new PropertyInternalList()
            {
                Name = "Sweden",
                InternalValue = "SE"
            };

            var cci = new PropertyInternalList()
            {
                Name = "Switzerland",
                InternalValue = "Switzerland"
            };

            var fka = new PropertyInternalList()
            {
                Name = "China",
                InternalValue = "CH"
            };

            var kso = new PropertyInternalList()
            {
                Name = "Finland",
                InternalValue = "FI"
            };

            var miwa = new PropertyInternalList()
            {
                Name = "Denmark",
                InternalValue = "DK"
            };


            var pgu = new PropertyInternalList()
            {
                Name = "Norway",
                InternalValue = "NO"
            };

            var rolu = new PropertyInternalList()
            {
                Name = "Sri Lanka",
                InternalValue = "SL"
            };


            listItem.Add(anbl);
            listItem.Add(cha);
            listItem.Add(cci);
            listItem.Add(fka);
            listItem.Add(kso);
            listItem.Add(miwa);
            listItem.Add(pgu);
            listItem.Add(rolu);

            return listItem;
        }

    }
}
