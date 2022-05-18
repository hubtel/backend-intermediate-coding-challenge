using System.Collections.Generic;
using System.Diagnostics;
using Hubtel.IntermediateCodingChallenge.Api.Controllers;
using Hubtel.IntermediateCodingChallenge.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Hubtel.IntermediateCodingChallenge.Api.Test
{
    public class SmsControllerTest
    {
        readonly SmsController _controller;

        public SmsControllerTest()
        {
            _controller = new SmsController(new Logger<SmsController>(new LoggerFactory(new List<ILoggerProvider>())));
        }

        [Fact]
        public void Send_Small_Batch_Size_Sms_Ok_Test()
        {
            //Arrange
            var batchSmsModel = new BatchSmsModel
            {
                From = "HubtelBackend",
                Content = "It is OK to be small",
                Contacts = new[]
                {
                    "233244778491"
                }
            };

            //Act
            var watch = new Stopwatch();
            watch.Start();

            var okResponse = _controller.BatchSms(batchSmsModel).GetAwaiter().GetResult();

            watch.Stop();

            //Assert
            Assert.IsType<OkObjectResult>(okResponse);

            //value of the result
            var item = okResponse as OkObjectResult;
            Assert.IsType<SmsResponse>(item?.Value);
            Assert.InRange(watch.ElapsedMilliseconds, 1, 500);

            //check value of model
            var smsResponse = item.Value as SmsResponse;
            Assert.Equal("request successfully submitted", smsResponse?.Message);
            Assert.NotNull(smsResponse?.BatchId);
        }

        //todo: this test is failing because of the high response time
        [Fact]
        public void Send_Large_Batch_Size_Sms_Ok_Test()
        {
            //Arrange
            var batchSmsModel = new BatchSmsModel
            {
                From = "HubtelBackend",
                Content = "It is OK to be large",
                Contacts = new[]
                {
                    "233244778491",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492",
                    "233244778492"
                }
            };

            //Act
            var watch = new Stopwatch();
            watch.Start();

            var okResponse = _controller.BatchSms(batchSmsModel).GetAwaiter().GetResult();

            watch.Stop();

            //Assert
            Assert.IsType<OkObjectResult>(okResponse);

            //value of the result
            var item = okResponse as OkObjectResult;
            Assert.IsType<SmsResponse>(item?.Value);
            
            // todo: important assertion
            Assert.InRange(watch.ElapsedMilliseconds, 1, 500);

            //check value of model
            var smsResponse = item.Value as SmsResponse;
            Assert.Equal("request successfully submitted", smsResponse?.Message);
            Assert.NotNull(smsResponse?.BatchId);
        }

        [Fact]
        public void SendBatchSms_BadRequest_Test()
        {
            //Arrange
            var batchSmsModel = new BatchSmsModel
            {
                From = "HubtelBackend",
                Content = "It is OK",
                Contacts = new string[6000]
            };

            //Act
            var badResponse = _controller.BatchSms(batchSmsModel).GetAwaiter().GetResult();

            //Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}