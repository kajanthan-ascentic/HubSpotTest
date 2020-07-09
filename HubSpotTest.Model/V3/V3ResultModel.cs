using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HubSpotTest.Model.V3
{
    public class V3ResultModel
    {

        public int total { get; set; }        public List<Result> results { get; set; }        public Pageing paging { get; set; }
    }


    public class Result    {        public string id { get; set; }        public long idLong { get { return long.Parse(id); } }        [JsonProperty("properties")]        public Dictionary<string, object> Properties { get; set; }        public DateTime createdAt { get; set; }        public DateTime updatedAt { get; set; }        public bool archived { get; set; }        public Associations associations { get; set; }    }

    public class Pageing    {        public Next next { get; set; }    }    public class Associations    {        public AssociationsTypes contacts { get; set; }        public AssociationsTypes deals { get; set; }        public AssociationsTypes companies { get; set; }            }    public class AssociationsTypes    {        public List<AssociationsResult> results { get; set; }    }    public class AssociationsResult    {        public string id { get; set; }        public string type { get; set; }    }    public class Next    {        public string after { get; set; }    }
}
