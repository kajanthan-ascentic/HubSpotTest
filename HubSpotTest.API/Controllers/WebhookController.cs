using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> logger;
        public WebhookController(ILogger<WebhookController> logger) 
        {
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] IList<TriggerObject> value) 
        {
            try
            {
                if (value != null)
                {
                    this.logger.LogInformation("Triggers Count", value.Count().ToString());

                    foreach (var item in value)
                    {
                        this.logger.LogInformation("Subcription type : " + item.subscriptionType);

                        switch (item.subscriptionType)
                        {
                            case "contact.creation":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                break;

                            case "contact.deletion":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                break;

                            case "company.creation":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                break;

                            case "company.deletion":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;

                            case "deal.creation":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;

                            case "deal.deletion":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;
                            case "contact.propertyChange":
                                this.logger.LogInformation("Property Name : " + item.propertyName.ToString());
                                this.logger.LogInformation("Property Value : " + item.propertyValue.ToString());
                                this.logger.LogInformation("Subcription Id : " + item.subscriptionId.ToString());
                                this.logger.LogInformation("Portal Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;

                            case "company.propertyChange":
                                this.logger.LogInformation("Property Name : " + item.propertyName.ToString());
                                this.logger.LogInformation("Property Value : " + item.propertyValue.ToString());
                                this.logger.LogInformation("Subcription Id : " + item.subscriptionId.ToString());
                                this.logger.LogInformation("Portal Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;

                            case "deal.propertyChange":
                                this.logger.LogInformation("Property Name : " + item.propertyName.ToString());
                                this.logger.LogInformation("Property Value : " + item.propertyValue.ToString());
                                this.logger.LogInformation("Subcription Id : " + item.subscriptionId.ToString());
                                this.logger.LogInformation("Portal Id : " + item.portalId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                break;




                            default:
                                break;
                        }
                    }
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}