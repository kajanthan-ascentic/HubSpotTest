using System;
namespace HubSpotTest.Model.V3
{
    public class AsscoaitionResponse
    {
        public AsscoaitionResponse()
        {
        }

        public int numErrors { get; set; }
        public string status { get; set; }
        public object results { get; set; }
    }
}
