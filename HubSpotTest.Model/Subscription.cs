using System;
namespace HubSpotTest.Model
{
    public class Subscription
    {
        public Subscription()
        {
        }

        public SubscriptionDetails subscriptionDetails { get; set; }

        public bool enabled { get; set; }
    }


    public class SubscriptionDetails {

        public string subscriptionType { get; set; }
        public string propertyName { get; set; }

    }

}
