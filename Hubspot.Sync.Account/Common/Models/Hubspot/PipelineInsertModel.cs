using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common.Models.Hubspot
{

    public class PipelineInsertModel
    {
        public string pipelineId { get; set; }
        public string label { get; set; }
        public int displayOrder { get; set; }
        public bool active { get; set; }
        public Stage[] stages { get; set; }

        public PipelineInsertModel() 
        {

        }

        public PipelineInsertModel(PipelineModel model)
        {
            this.pipelineId = model.pipelineId;
            this.label = model.label;
            this.displayOrder = model.displayOrder;
            this.active = model.active;
            this.stages = model.stages;
        }
    }

}
