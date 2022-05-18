using System;
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
        private int _maxBatchSize = 5000;

        public SmsController(ILogger<SmsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> BatchSms([FromBody] BatchSmsModel request)
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

                foreach (var contact in request.Contacts)
                {
                    await SendSms(new SubmitSmsRequest
                    {
                        From = request.From,
                        Content = request.Content,
                        To = contact,
                        MessageId = Guid.NewGuid().ToString(),
                        BatchId = batchId
                    });
                }
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

        private async Task SendSms(SubmitSmsRequest smsRequest)
        {
            await Task.Delay(100);
            _logger.LogDebug("message {content} sent to {to} with batch id {batch_id}", smsRequest.Content, smsRequest.To, smsRequest.BatchId);
        }
    }
}