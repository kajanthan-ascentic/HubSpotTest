using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HubSpotTest.Service.Service
{
    public class HotSpotApiService: IHotSpotApiService
    {
        private HttpClient client = new HttpClient();
        private readonly IHttpClinentService httpclient;
        public HotSpotApiService(IHttpClinentService httpclient)
        {
            this.httpclient = httpclient;
        }

        public async Task<dynamic> GetAllContacts()
        {
            var contactlist = await httpclient.GetAsync("/contacts/v1/lists/all/contacts/all");
            dynamic response = JsonConvert.DeserializeObject(contactlist);

            return response;
        }

        public async Task<string> CreateContact(ContactModel contact)
        {
            var jsonObj = JsonConvert.SerializeObject(contact);
            var response = await httpclient.PostAsync("/contacts/v1/contact/", jsonObj);
            return response;
        }


        public async Task<string> UpdateContact(string id,ContactModel contact)
        {
            var jsonObj = JsonConvert.SerializeObject(contact);
            var response = await httpclient.PostAsync("/contacts/v1/contact/vid/" + id + "/profile", jsonObj);
            return response;
        }


        public async Task<string> DeleteContact(string id)
        {
            var response = await httpclient.DeleteAsync("/contacts/v1/contact/vid/" + id);
            return response;
        }

        public async Task<dynamic> GetContactById(string id)
        {
            var response = await httpclient.GetAsync("/contacts/v1/contact/vid/" + id + "/profile");
            return response;
        }

        public async Task<dynamic> GetAllCompanies()
        {
            var companylist = await httpclient.GetAsync("/companies/v2/companies/paged?&properties=name&properties=description&properties=website&limit=15");
            dynamic response = JsonConvert.DeserializeObject(companylist);

            return response;
        }

        public async Task<dynamic> GetCompanyById(string id)
        {

            var response = await httpclient.GetAsync("/companies/v2/companies/" + id);
            return response;
        }

        public async Task<string> CreateCompany(CompanyModel company)
        {
            var jsonObj = JsonConvert.SerializeObject(company);
            var response = await httpclient.PostAsync("/companies/v2/companies", jsonObj);
            return response;
        }

        public async Task<string> UpdateCompany(string id, CompanyModel company)
        {
            var jsonObj = JsonConvert.SerializeObject(company);
            var response = await httpclient.PutAsync("/companies/v2/companies/" + id , jsonObj);
            return response;
        }

        public async Task<string> DeleteCompany(string id)
        {
            var response = await httpclient.DeleteAsync("/companies/v2/companies/" + id);
            return response;
        }




        public async Task<string> SubscriptionType(Subscription subcription)
        {
            var jsonObj = JsonConvert.SerializeObject(subcription);
            var response = await httpclient.SubcriptionPost(jsonObj);
            return response;
        }

    }
}
