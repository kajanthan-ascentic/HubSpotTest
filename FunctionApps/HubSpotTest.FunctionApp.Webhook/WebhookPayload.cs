using System;
namespace HubSpotTest.FunctionApp.Webhook
{
    public class WebhookPayload
    {
        public WebhookPayload()
        {
        }

        public string eventId { get; set; }
        public int subscriptionId { get; set; }
        public int portalId { get; set; }
        public DateTime occurredAt { get; set; }
        public string subscriptionType { get; set; }
        public int attemptNumber { get; set; }
        public int objectId { get; set; }
        public string changeSource { get; set; }
        public string propertyName { get; set; }
        public string propertyValue { get; set; }
    }
}
