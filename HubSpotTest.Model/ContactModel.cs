using System;
using System.Collections.Generic;

namespace HubSpotTest.Model
{
    public class ContactModel
    {
        public ContactModel()
        {
        }

        public IList<HubSpotPropertyModal> properties { get; set; }
    }
}
