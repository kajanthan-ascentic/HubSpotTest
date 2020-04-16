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
        public IActionResult Post([FromBody] HubspotWebhookTriggerModel value) 
        {
            try
            {
                if (value != null)
                {
                    this.logger.LogInformation("Triggers Count", value.Triggers.Count());

                    foreach (var item in value.Triggers)
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
                                break;

                            case "deal.creation":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
                                break;

                            case "deal.deletion":
                                this.logger.LogInformation("Event Id : " + item.eventId.ToString());
                                this.logger.LogInformation("Object Id : " + item.objectId.ToString());
                                this.logger.LogInformation("Port Id : " + item.portalId.ToString());
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