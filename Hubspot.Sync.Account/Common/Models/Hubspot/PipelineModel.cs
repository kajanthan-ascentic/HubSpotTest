using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common.Models.Hubspot
{

    public class PipelineModel
    {
        public string pipelineId { get; set; }
        public string objectType { get; set; }
        public string label { get; set; }
        public int displayOrder { get; set; }
        public bool active { get; set; }
        public Stage[] stages { get; set; }
    }

    public class Stage
    {
        public string stageId { get; set; }
        public string label { get; set; }
        public int displayOrder { get; set; }
        public Metadata metadata { get; set; }
        public bool active { get; set; }
    }

    public class Metadata
    {
        public float probability { get; set; }
    }

}
