using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubSpotTest.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        #region Members 

        private readonly ITokenService _tokenService;

        #endregion

        #region Constructor

        public OAuthController(ITokenService tokenService) 
        {
            this._tokenService = tokenService;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Get(string code) 
        {
            var result = await this._tokenService.GetAccessToken(code);

            return Ok(result);

            /*
            try
            {
                var retult = this._tokenService.GetToken(code);
                return Ok(retult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            */
        }
    }
}