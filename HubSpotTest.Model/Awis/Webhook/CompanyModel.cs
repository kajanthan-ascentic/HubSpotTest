using System;
using System.Collections.Generic;
using System.Text;

namespace HubSpotTest.Model.Awis.Webhook
{
    public class CompanyModel : BaseModel
    {
        public string OrgOrSecurityNo { get; set; }

        public string FirstName { get; set; }

        public string OrgOrLastName { get; set; }

        public bool IsActive { get; set; }
    }
}
