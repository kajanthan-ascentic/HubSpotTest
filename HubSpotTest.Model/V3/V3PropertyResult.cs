using System;
using System.Collections.Generic;

namespace HubSpotTest.Model.V3
{
    public class V3PropertyResult
    {
        public V3PropertyResult()
        {
        }

        public List<PropertyResult> results { get; set; }
    }

    public class PropertyResult
    {
        public string name { get; set; }
        public string label { get; set; }
        public string groupName { get; set; }

        public ModificationMetaData modificationMetadata { get; set; }
    }

    public class ModificationMetaData
    {
        public bool readOnlyValue { get; set; }
        public bool archivable { get; set; }
        public bool readOnlyDefinition { get; set; }
    }

}
