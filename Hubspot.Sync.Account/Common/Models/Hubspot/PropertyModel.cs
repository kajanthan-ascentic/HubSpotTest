using System;
using System.Collections.Generic;
using System.Text;

namespace Hubspot.Sync.Account.Common.Models.Hubspot
{

    public class PropertyModel
    {
        public string groupName { get; set; }
        public bool hidden { get; set; }
        public Modificationmetadata modificationMetadata { get; set; }
        public string name { get; set; }
        public int displayOrder { get; set; }
        public Option[] options { get; set; }
        public string label { get; set; }
        public bool hasUniqueValue { get; set; }
        public string type { get; set; }
        public string fieldType { get; set; }
    }

    public class Modificationmetadata
    {
        public bool readOnlyOptions { get; set; }
        public bool readOnlyValue { get; set; }
        public bool readOnlyDefinition { get; set; }
        public bool archivable { get; set; }
    }

    public class Option
    {
        public string label { get; set; }
        public string description { get; set; }
        public string value { get; set; }
        public int displayOrder { get; set; }
        public bool hidden { get; set; }
    }

    public class PropertyModelInsert
    {
        public string name { get; set; }
        public string groupName { get; set; }
        public bool hidden { get; set; }
        public int displayOrder { get; set; }
        public Option[] options { get; set; }
        public string label { get; set; }
        public bool hasUniqueValue { get; set; }
        public string type { get; set; }
        public string fieldType { get; set; }

        public PropertyModelInsert() 
        {

        }

        public PropertyModelInsert(PropertyModel model)
        {
            this.name = model.name;
            this.groupName = model.groupName;
            this.hidden = model.hidden;
            this.displayOrder = model.displayOrder;
            this.options = model.options;
            this.label = model.label;
            this.hasUniqueValue = model.hasUniqueValue;
            this.type = model.type;
            this.fieldType = model.fieldType;
        }

    }
}
