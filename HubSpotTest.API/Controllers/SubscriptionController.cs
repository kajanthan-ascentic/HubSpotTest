using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly IHotSpotApiService hotspotservice;
        private readonly ILogger<SubscriptionController> logger;
        public SubscriptionController(ILogger<SubscriptionController> logger, IHotSpotApiService hotspotservice)
        {
            this.logger = logger;
            this.hotspotservice = hotspotservice;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values/5
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Subscription value)
        {
            try
            {
                var response = await hotspotservice.SubscriptionType(value);
                return Ok(response);
            }
            catch (Exception ex)
            {
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
