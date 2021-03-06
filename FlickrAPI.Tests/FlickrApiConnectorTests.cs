﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using FlickrAPI.Core.Interfaces;
using FlickrAPI.Core.Model;
using FlickrAPI.Core.Services;
using FlickrAPI.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlickrAPI.Tests
{

    public class FlickrApiConnectorTests
    {
        private readonly Mock<HttpMessageHandler> handlerMock;
        private readonly HttpResponseMessage response;
        private readonly Mock<IHttpClientFactory> mockFactory;
        private readonly Mock<IServiceLogger> mockLog;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<IGenericApiConnector<Root>> mockGenericApi;
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
            mockLog = new Mock<IServiceLogger>();
            mockConfiguration = new Mock<IConfiguration>();
            mockGenericApi = new Mock<IGenericApiConnector<Root>>();
            _unitOfWork = new Mock<IUnitOfWork>();

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            
            mockLog.Setup(x => x.LogToFile(It.IsAny<string>(), It.IsAny<string>()));

            var httpClient = new HttpClient(handlerMock.Object);
            mockFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        }

        [Fact]
        public void GetPictures_CheckIfControllerReturns400_WhenInputStringIsEmpty()
        {
            //Arrange
            var controller = new FlickrController(_unitOfWork.Object);

            //Act
            var response = controller.GetPicturesAsync("");
            var value = response.Result as BadRequestResult;

            //Assert
            Assert.Equal(400, value.StatusCode);            
        }

        [Fact]
        public async Task GetPictures_CheckIfControllerReturns200_WhenInputStringIsPopulatedAsync()
        {
            //Arrange
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "url")]).Returns("mock value");
            mockConfiguration.Setup(x => x.GetSection("ApiCallSettings")).Returns(mockConfSection.Object);
            var url = @"https://www.flickr.com/{searchString}&tag_mode=&format=json";
            _unitOfWork.Setup(x => x.SearchRootData).Returns(new GenericApiConnector<Root>(mockLog.Object,mockFactory.Object,mockConfiguration.Object,url,"any path"));

            //Act
            var controller = new FlickrController(_unitOfWork.Object);
            var response = controller.GetPicturesAsync("some value");
            var value = response.Result as OkObjectResult;

            //Assert
            Assert.Equal(200, value.StatusCode);
        }
    }
}
