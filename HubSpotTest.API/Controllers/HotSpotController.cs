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
    public class HotSpotController : Controller
    {
        private readonly ITokenService hotspotservice;
        public HotSpotController(ITokenService hotspotservice)
        {
            this.hotspotservice = hotspotservice;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAsync(string code)
        {
            try
            {
                var retult  = this.hotspotservice.GetToken(code);
                return Ok(retult);
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
        public  IActionResult PostAsync([FromBody]ContactModel value)
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]ContactModel value)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public  IActionResult DeleteAsync(string id)
        {
            try
            {
               // var response = await hotspotservice.DeleteContact(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
