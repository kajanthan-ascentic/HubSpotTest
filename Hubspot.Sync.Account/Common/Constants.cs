using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common
{
    public class Constants
    {
        public const string URL_API = "https://api.hubapi.com";

        public const string URL_PROPERTY_GROUP = "/crm/v3/properties/{0}/groups";
        public const string URL_PROPERTY = "/crm/v3/properties/{0}";
        public const string URL_PIPELINE = "/crm-pipelines/v1/pipelines/{0}";

        public const string KEY_API_DEV = "bb7a510a-73dc-4b73-96ab-4c78987c6485";
        public const string KEY_API_QA = "a6f4c218-809e-4f7a-b24c-f83e8e816aec";
        public const string KEY_API_LIVE = "";
    }
}
