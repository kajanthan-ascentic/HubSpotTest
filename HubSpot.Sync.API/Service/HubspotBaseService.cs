using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.Sync.API.Model.Settings;
using HubSpot.Sync.Service.Interface;
using HubSpotTest.Model.V3;
using Microsoft.Extensions.Options;

namespace HubSpot.Sync.API.Service
{
    public abstract class HubspotBaseService
    {
        protected readonly IOptions<SyncPropertyMapperSettings> syncPropertySettings;        private readonly ICompanyService companyService;        private readonly IDealService dealService;        private readonly IContactService contactService;        public HubspotBaseService(IOptions<SyncPropertyMapperSettings> syncPropertySettings, ICompanyService companyService,
            IContactService contactService, IDealService dealService)
        {            this.syncPropertySettings = syncPropertySettings;            this.companyService = companyService;            this.dealService = dealService;            this.contactService = contactService;        }


        public async Task<List<Result>> GetAllCompanies(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.companyService.GetAllCompanies(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }

        public async Task<List<Result>> GetExistingCompanies(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.companyService.GetAllExistingCompanies(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }

        public async Task<List<Result>> GetAllDeals(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.dealService.GetAllDeals(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }

        public async Task<List<Result>> GetExistingDeals(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.dealService.GetAllExistingDeals(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }

        public async Task<List<Result>> GetAllContacts(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.contactService.GetAllContacts(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }

        public async Task<List<Result>> GetExistingContacts(string properties)
        {
            List<Result> result = new List<Result>();

            string after = "0";            V3ResultModel v3Result = null;

            do            {

                v3Result = await this.contactService.GetAllExistingContacts(this.syncPropertySettings.Value.HubspotDataDefaults.SearchApiDefaults.Limit, after, properties);
                if (v3Result != null)                {                    result.AddRange(v3Result.results);                    if (v3Result.paging != null &&                        v3Result.paging.next != null &&                        !String.IsNullOrEmpty(v3Result.paging.next.after))                    {                        after = v3Result.paging.next.after;

                        // logger.LogInformation("GetAllSearch object : " + hubspotObject + " after:" + after);

                        // delay after creation to avoid api rate limit 
                        // await this.DelayForHubspotAPI<T>(logger);
                    }                }            }            while (v3Result != null && v3Result.paging != null);

            return result;
        }


        public AssociationBatchCreationSub GenerateAssociationProperty(long toId, long fromId, string type)        {            AssociationBatchCreationSub associationBatchSub = new AssociationBatchCreationSub();            var expandoObjectFromDic = new ExpandoObject() as IDictionary<string, object>;            var expandoObjectToDic = new ExpandoObject() as IDictionary<string, object>;            associationBatchSub.from = new ExpandoObject();            associationBatchSub.to = new ExpandoObject();            expandoObjectFromDic.Add("id", fromId);            expandoObjectToDic.Add("id", toId);            associationBatchSub.from = (ExpandoObject)expandoObjectFromDic;            associationBatchSub.to = (ExpandoObject)expandoObjectToDic;            associationBatchSub.type = type;            return associationBatchSub;        }

        public Result GetDealDataByExisting(List<Result> existingDeals, Result deal)        {            var dealid = deal.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID);            return existingDeals.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID, out object value) &&                                    value.ToString() == dealid.ToString()).FirstOrDefault();        }


        public Result GetCompanyInformation(List<Result> existingCompanies, Result company)        {            var customerId = company.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber);            return existingCompanies.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Company.HubspotProperties.CustomerNumber, out object value) &&                                    value.ToString() == customerId.ToString()).FirstOrDefault();        }




        public Result GetContactInformation(List<Result> existingContacts, List<Result> contactList, long contactId)        {
            var contact = contactList.Where(x => x.idLong == contactId).FirstOrDefault();
            if (contact != null)
            {
                var customerId = contact.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Contact.HubspotProperties.Email);
                return existingContacts.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Contact.HubspotProperties.Email, out object value) &&
                                        value.ToString() == customerId.ToString()).FirstOrDefault();
            }
            return null;
        }



        public Result GetDealInformation(List<Result> existingDealsList, List<Result> dealList, long dealid)        {
            var deal = dealList.Where(x => x.idLong == dealid).FirstOrDefault();
            if (deal != null)
            {
                var customerId = deal.Properties.GetValueOrDefault(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID);
                return existingDealsList.Where(x => x.Properties.TryGetValue(this.syncPropertySettings.Value.Deal.HubspotProperties.InsuranceID, out object value) &&
                                        value.ToString() == customerId.ToString()).FirstOrDefault();
            }
            return null;
        }
    }
}
