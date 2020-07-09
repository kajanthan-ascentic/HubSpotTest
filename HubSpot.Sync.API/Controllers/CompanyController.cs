using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpot.Sync.API.Interface;
using HubSpot.Sync.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HubSpot.Sync.API.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyHubspotService companyHubspotService;
        private readonly ILogger<CompanyController> logger;
        public CompanyController(ICompanyHubspotService companyHubspotService, ILogger<CompanyController> logger)
        {
            this.companyHubspotService = companyHubspotService;
            this.logger = logger;
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

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VerifyModal modal)
        {
            try
            {
                if (modal.Name.ToString() == "Test")
                {
                    await this.companyHubspotService.CreateCompanies();
                    return Ok("success");
                }
                else
                {
                    this.logger.LogInformation("Hubspot Value Different");
                    return StatusCode(404, "Hubspot Value Different");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
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
