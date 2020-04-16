using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        public WebhookController() 
        {

        }

        public IActionResult Post([FromBody] HubspotWebhookTriggerModel value) 
        {

        }
    }
}