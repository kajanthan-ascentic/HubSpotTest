using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Model.Awis.Webhook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwisDatabaseWebhookController : ControllerBase
    {
        private readonly ILogger<AwisDatabaseWebhookController> _logger;

        public AwisDatabaseWebhookController(ILogger<AwisDatabaseWebhookController> logger) 
        {
            this._logger = logger;
        }

        // GET: api/AwisDatabaseWebhook
        [HttpGet]
        public IActionResult Get()
        {
            this._logger.LogInformation("Api Working");

            return Ok(new  { Respons = "Api Working" });
        }

        // POST: api/AwisDatabaseWebhook
        [HttpPost("Company")]
        public IActionResult Post([FromBody] CompanyModel companyModel)
        {
            this._logger.LogInformation(JsonConvert.SerializeObject(companyModel));

            return Ok(companyModel);
        }
    }
}
