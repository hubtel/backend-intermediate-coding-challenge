using System;
using System.Threading;
using System.Threading.Tasks;
using Hubtel.IntermediateCodingChallenge.Api.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hubtel.IntermediateCodingChallenge.Api
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (!RequestQueue.SmsQueue.IsEmpty)
                {
                    var result = RequestQueue.SmsQueue.TryDequeue(out var request);
                    if (result)
                    {
                        foreach (var contact in request.Item2.Contacts)
                        {
                            await SendSms(new SubmitSmsRequest
                            {
                                BatchId = request.Item1,
                                Content = request.Item2.Content,
                                From = request.Item2.From,
                                To = contact,
                                MessageId = Guid.NewGuid().ToString()
                            });
                        }
                    }
                }
                
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task SendSms(SubmitSmsRequest smsRequest)
        {
            await Task.Delay(100);
            _logger.LogDebug("message {content} sent to {to} with batch id {batch_id}", smsRequest.Content,
                smsRequest.To, smsRequest.BatchId);
        }
    }
}