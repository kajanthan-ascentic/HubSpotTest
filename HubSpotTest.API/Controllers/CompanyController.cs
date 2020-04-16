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
    public class CompanyController : Controller
    {
        private readonly IHotSpotApiService hotspotservice;
        private readonly ILogger<CompanyController> logger;
        public CompanyController(IHotSpotApiService hotspotservice, ILogger<CompanyController> logger)
        {
            this.logger = logger;
            this.hotspotservice = hotspotservice;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                this.logger.LogInformation("get Companies Information");
                var response = await hotspotservice.GetAllCompanies();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var response = await hotspotservice.GetCompanyById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CompanyModel value)
        {
            try
            {
                var response = await hotspotservice.CreateCompany(value);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]CompanyModel value)
        {
            try
            {
                var response = await hotspotservice.UpdateCompany(id, value);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var response = await hotspotservice.DeleteCompany(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
