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

        public const string KEY_API_DEV = "36c767c6-7b8a-4d10-870f-e597474ac056";
        public const string KEY_API_QA = "d47ee2a5-29a1-4f02-9655-0d66c0da4f23";
        public const string KEY_API_LIVE = "";
    }
}
