using System;
using System.Collections.Generic;
using System.Dynamic;

namespace HubSpotTest.Model.V3
{
    public class ContactExpandoObject
    {
        public ContactExpandoObject()
        {
        }

        public List<SubContactExpandoObject> results { get; set; }
    }


    public class SubContactExpandoObject
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public bool archived { get; set; }

        public ExpandoObject properties { get; set; }
    }
}
