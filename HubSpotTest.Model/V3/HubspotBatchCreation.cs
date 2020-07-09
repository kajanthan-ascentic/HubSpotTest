using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HubSpotTest.Model.V3
{
    public class HubspotBatchCreation
    {
        public HubspotBatchCreation()
        {
        }

        public List<HubspotBatchCreateProperty> inputs { get; set; }
    }


    public class HubspotBatchCreateProperty
    {
        [JsonProperty("properties")]        public Dictionary<string, object> Properties { get; set; }
    }
}
