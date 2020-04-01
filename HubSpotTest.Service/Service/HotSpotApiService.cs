using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HubSpotTest.Service.Interface;
using Newtonsoft.Json;

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
    }
}
