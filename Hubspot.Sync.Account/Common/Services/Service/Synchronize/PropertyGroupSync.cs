using Hubspot.Sync.Account.Common.Models.Hubspot;
using Hubspot.Sync.Account.Common.Services.Service.Hubspot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hubspot.Sync.Account.Common.Services.Service.Synchronize
{
    public class PropertyGroupSync : BaseSynchronize<PropertyGroup>
    {
        public PropertyGroupSync(string sourceAPIKey, 
                                    string destinationAPIKey, 
                                    string objectType) : base(sourceAPIKey, 
                                                              destinationAPIKey, 
                                                              objectType, 
                                                              Constants.URL_PROPERTY_GROUP) 
        {
            
        }
        protected override string GetConsoleText(PropertyGroup model)
        {
            return model.name;
        }

        protected override object GetInsertModel(PropertyGroup model)
        {
            return model;
        }

        protected override IEnumerable<PropertyGroup> GetMissingList()
        {
            HashSet<string> destinationLabels = new HashSet<string>(this._destinationList.Select(x => x.name));

            return this._sourceList.Where(x => !destinationLabels.Contains(x.name)).ToList();
        }

        protected override IEnumerable<PropertyGroup> GetOrderList(IEnumerable<PropertyGroup> list)
        {
            return list.OrderBy(x => x.displayOrder);
        }
    }
}
