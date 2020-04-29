using Hubspot.Sync.Account.Common.Models.Hubspot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hubspot.Sync.Account.Common.Services.Service.Synchronize
{
    public class PipelineSync : BaseSynchronize<PipelineModel>
    {
        public PipelineSync(string sourceAPIKey,
                                    string destinationAPIKey,
                                    string objectType,
                                    bool syncEnabled = false) : base(sourceAPIKey,
                                                              destinationAPIKey,
                                                              objectType,
                                                              Constants.URL_PIPELINE,
                                                              syncEnabled)
        {

        }
        protected override string GetConsoleText(PipelineModel model)
        {
            return model.label;
        }

        protected override object GetInsertModel(PipelineModel model)
        {
            return model;
        }

        protected override IEnumerable<PipelineModel> GetMissingList()
        {
            HashSet<string> destinationLabels = new HashSet<string>(this._destinationList.Select(x => x.label));

            return this._sourceList.Where(x => !destinationLabels.Contains(x.label)).ToList();
        }

        protected override IEnumerable<PipelineModel> GetOrderList(IEnumerable<PipelineModel> list)
        {
            return list.OrderBy(x => x.displayOrder);
        }
    }
}
