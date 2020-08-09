using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.Sync.API.Interface;
using HubSpot.Sync.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubSpot.Sync.API.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactHubspotService contactHubspotService;
        private readonly ILogger<ContactController> logger;
        public ContactController(IContactHubspotService contactHubspotService, ILogger<ContactController> logger)
        {
            this.contactHubspotService = contactHubspotService;
            this.logger = logger;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAsync()
        {
            try
            {
                // await this.contactHubspotService.CreateContacts();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VerifyModal modal)
        {
            try
            {
                if (modal.Name.ToString() == "Test")
                {
                    await this.contactHubspotService.CreateContacts();
                    return Ok("success");
                }else
                {
                    this.logger.LogInformation("Hubspot Value Different");
                    return StatusCode(404, "Hubspot Value Different");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                this.logger.LogInformation(JsonConvert.SerializeObject(ex));
                this.logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
