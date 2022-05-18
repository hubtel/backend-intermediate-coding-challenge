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
    [Route("api/sms")]
    public class SmsController : ControllerBase
    {

        private readonly ILogger<SmsController> _logger;
        private int _maxBatchSize = 5;

        public SmsController(ILogger<SmsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public IActionResult BatchSms([FromBody] BatchSmsModel request)
        {
            try
            {
                if (request.Contacts.Length > _maxBatchSize)
                {
                    _logger.LogError("invalid batch size. maximum limit is {batch_size}", _maxBatchSize);

                    return BadRequest(new SmsResponse
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = $"invalid batch size. maximum limit is {_maxBatchSize}"
                    });
                }

                var batchId = Guid.NewGuid().ToString();
                
                _logger.LogInformation("successfully processed batch {batch_id}", batchId);
                return Ok(new SmsResponse
                {
                    BatchId = batchId,
                    Message = "request successfully submitted"
                });
            }
            catch (Exception e)
            {
                _logger.LogError("exception {error}", e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new SmsResponse
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = "oops.. request cannot be processed!"
                });
            }
        }
    }
}