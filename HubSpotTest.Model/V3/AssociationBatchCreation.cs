using System;
using System.Collections.Generic;
using System.Dynamic;

namespace HubSpotTest.Model.V3
{
    public class AssociationBatchCreation
    {
        public AssociationBatchCreation()
        {
        }


        public List<AssociationBatchCreationSub> inputs { get; set; }
    }

    public class AssociationBatchCreationSub    {        public ExpandoObject from { get; set; }        public ExpandoObject to { get; set; }        public string type { get; set; }    }
}
