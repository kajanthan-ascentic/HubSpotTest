using System;
using System.Collections.Generic;

namespace HubSpotTest.Model.V3
{
    public class AssociationBatchResponse
    {
        public List<AssociationBatchCreationSub> results { get; set; }        public int numErrors { get; set; }        public string status { get; set; }
    }
}
