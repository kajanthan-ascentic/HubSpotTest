using Hubspot.Sync.Account.Common.Models.Hubspot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubspot.Sync.Account.Common.Services.Service.Synchronize
{
    public class PropertySync : BaseSynchronize<PropertyModel>
    {
        public PropertySync(string sourceAPIKey,
                                    string destinationAPIKey,
                                    string objectType,
                                    bool syncEnabled = false) : base(sourceAPIKey,
                                                              destinationAPIKey,
                                                              objectType,
                                                              Constants.URL_PROPERTY,
                                                              syncEnabled)
        {

        }

        protected override string GetConsoleText(PropertyModel model)
        {
            return model.name + "(" + model.label + ")";
        }

        protected override object GetInsertModel(PropertyModel model)
        {
            return new PropertyModelInsert(model);
        }

        protected override IEnumerable<PropertyModel> GetMissingList()
        {
            IEnumerable<PropertyModel> destination = this._destinationList.ToList().FindAll(x => x.groupName.Contains("ac_") || x.groupName.Contains("autoconcept_"));
            HashSet<string> destinationLabels = new HashSet<string>(destination.Select(x => x.name));

            IEnumerable<PropertyModel> source = this._sourceList.ToList().FindAll(x => x.groupName.Contains("ac_") || x.groupName.Contains("autoconcept_"));

            return source.Where(x => !destinationLabels.Contains(x.name)).ToList();
        }

        protected override IEnumerable<PropertyModel> GetOrderList(IEnumerable<PropertyModel> list)
        {
            return list.OrderBy(x => x.displayOrder);
        }
    }
}
