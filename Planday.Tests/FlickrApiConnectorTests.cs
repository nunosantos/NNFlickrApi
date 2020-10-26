﻿using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Planday.Core.Interfaces;
using Planday.Core.Model;
using Planday.Core.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Planday.Tests
{

    public class FlickrApiConnectorTests
    {
        private readonly Mock<HttpMessageHandler> handlerMock;
        private HttpResponseMessage response;
        private readonly Mock<IHttpClientFactory> mockFactory;
        private readonly Mock<IServiceLogger> mockLog;
        public StringContent data;

        public FlickrApiConnectorTests()
        {
            data = new StringContent(@"{
  ""photos"": {
    ""page"": 1,
    ""pages"": 4428,
    ""perpage"": 100,
    ""total"": ""442796"",
    ""photo"": [
      {
        ""id"": ""50531357567"",
        ""owner"": ""62938898@N00"",
        ""secret"": ""1cd5105c71"",
        ""server"": ""65535"",
        ""farm"": 66,
        ""title"": ""Suburban sky"",
        ""ispublic"": 1,
        ""isfriend"": 0,
        ""isfamily"": 0
      },
      {
        ""id"": ""50531333417"",
        ""owner"": ""171415767@N06"",
        ""secret"": ""73e0a75852"",
        ""server"": ""65535"",
        ""farm"": 66,
        ""title"": ""Qiston py gaari lainy ka hukam | Sheikh Maqsood ul Hassan Faizi | 24SevenIslam"",
        ""ispublic"": 1,
        ""isfriend"": 0,
        ""isfamily"": 0
      }
    ]
  },
  ""stat"": ""ok""
}
");
            handlerMock = new Mock<HttpMessageHandler>();
            response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = data
            };
            mockFactory = new Mock<IHttpClientFactory>();
            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            mockLog = new Mock<IServiceLogger>();
            mockLog.Setup(x => x.LogToFile(It.IsAny<string>()));

            var httpClient = new HttpClient(handlerMock.Object);
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

        [Fact]
        public async Task GetPictures_CheckIfServiceThrowsException_WhenInputStringIsEmptyAsync()
        {
            //string value = "      ";
            //var connector = new ApiConnector(mockLog.Object, mockFactory.Object);

            ////Func<Task> act = () => connector.GetAsync(value,"","");

            //var exception = await Assert.ThrowsAsync<ArgumentNullException>(act);
            //await Assert.ThrowsAsync<ArgumentNullException>(() => connector.GetAsync(value, "flickr", ""));
        }

        [Fact]
        public void GetPictures_CheckIfServiceReturnsAnyData_WhenInputStringIsPopulated()
        {
            //string value = "boat";
            //var url = "http://something.com";
            //var connector = new ApiConnector(mockLog.Object, mockFactory.Object);
            //var connectorResponse =  connector.GetAsync(value, "flickr", url);
            //Assert.NotNull(connectorResponse.Result);
        }
    }
}