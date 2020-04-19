using System;
using System.Collections.Generic;
using System.Text;

namespace HubSpotTest.Model.Awis.Webhook
{
    public class BaseModel
    {
        public enum ChangeType 
        {
            Insert  = 1,
            Update  = 2,
            Delete  = 3
        }

        public int ID { get; set; }

        public ChangeType Change { get; set; }
    }
}
