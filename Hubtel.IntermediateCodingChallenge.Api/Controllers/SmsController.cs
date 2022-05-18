using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hubtel.IntermediateCodingChallenge.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hubtel.IntermediateCodingChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsController : ControllerBase
    {

        private readonly ILogger<SmsController> _logger;

        public SmsController(ILogger<SmsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public IActionResult BatchSms([FromBody] BatchSmsModel request)
        {
            try
            {
                return Ok(new
                {
                    batchId = Guid.NewGuid().ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError("exception {error}", e.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}