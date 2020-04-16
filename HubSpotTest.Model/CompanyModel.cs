using System;
using System.Collections.Generic;

namespace HubSpotTest.Model
{
    public class CompanyModel
    {
        public CompanyModel()
        {
        }

        public IList<CompanyProperty> properties { get; set; }
    }
}
