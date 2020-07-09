using System;
namespace HubSpotTest.Model.HubSpot
{

    public class HubspotResponseModel<T>    {






        /// <summary>        /// Gets if the response is valid        /// </summary>        public bool Status { get; set; }







        /// <summary>        /// Gets the valid response data        /// </summary>        public T Data { get; set; }







        /// <summary>        /// Get the invalid response data        /// </summary>        public HubspotErrorResponse ErrorData { get; set; }    }    public class HubspotErrorResponse    {        public Validationresult[] validationResults { get; set; }        public string status { get; set; }        public string message { get; set; }        public string correlationId { get; set; }        public string requestId { get; set; }    }    public class Validationresult    {        public bool isValid { get; set; }        public string message { get; set; }        public string error { get; set; }        public string name { get; set; }    }
}
