using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Model;
using HubSpotTest.Service.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    public class HubSpotContactController : Controller
    {
        private readonly IHotSpotApiService hotspotservice;
        public HubSpotContactController(IHotSpotApiService hotspotservice)
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
        public async Task<IActionResult> PostAsync([FromBody]Contact data)
        {
            try
            {
                var response = await hotspotservice.AddContact(data);
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
