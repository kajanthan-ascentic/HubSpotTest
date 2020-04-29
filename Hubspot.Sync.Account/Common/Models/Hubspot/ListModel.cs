using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common.Models.Hubspot
{
    public class ListModel<T>
    {
        public IEnumerable<T> results { get; set; }
    }
}
