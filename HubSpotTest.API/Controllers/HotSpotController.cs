using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Service.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    public class HotSpotController : Controller
    {
        private readonly IHotSpotApiService hotspotservice;
        public HotSpotController(IHotSpotApiService hotspotservice)
        {
            this.hotspotservice = hotspotservice;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = await hotspotservice.GetAllContacts();
                return Ok(response);
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
        public void Post([FromBody]string value)
        {
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
