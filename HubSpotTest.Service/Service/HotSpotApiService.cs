using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Newtonsoft.Json;
using JsonProperty = HubSpotTest.Model.JsonProperty;

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
            dynamic stuff = JsonConvert.DeserializeObject(contactlist);

            return stuff;
        }


        public async Task<dynamic> AddContact(Contact contact)
        {
            var listProperty = new List<JsonProperty>();
            foreach (var property in typeof(Contact).GetProperties())
            {
                var data = property;
                var propertyInfo = contact.GetType().GetProperty(property.Name);
                listProperty.Add(new JsonProperty()
                {
                    property = property.Name,
                    value = propertyInfo.GetValue(contact, null)

                }); ;
            }

            var contactlist = await httpclient.PostAsync("/contacts/v1/lists/all/contacts/all", null);
            dynamic stuff = JsonConvert.DeserializeObject(contactlist);

            return stuff;
        }

    }
}
