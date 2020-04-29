using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common.Models.Hubspot
{
    public class PropertyGroup
    {
        public string name { get; set; }
        public string label { get; set; }
        public int displayOrder { get; set; }
        public bool archived { get; set; }
    }

}
