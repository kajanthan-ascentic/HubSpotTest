using System;
namespace HubSpotTest.Model.V3
{
    public class V3ResponseResult
    {
        public V3ResponseResult()
        {
        }

        public object Data { get; set; }

        public object ErrorData { get; set; }

        public bool Status { get; set; }
    }
}
