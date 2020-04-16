using System;
using System.Collections.Generic;
using System.Text;

namespace HubSpotTest.Model
{
    public class HubspotWebhookTriggerModel
    {
        public TriggerObject[] Triggers { get; set; }
    }

    public class TriggerObject
    {
        public int objectId { get; set; }
        public string propertyName { get; set; }
        public string propertyValue { get; set; }
        public string changeSource { get; set; }
        public long eventId { get; set; }
        public int subscriptionId { get; set; }
        public int portalId { get; set; }
        public int appId { get; set; }
        public long occurredAt { get; set; }
        public string subscriptionType { get; set; }
        public int attemptNumber { get; set; }
    }

}
