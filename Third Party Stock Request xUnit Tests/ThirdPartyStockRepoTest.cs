using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RequestStockService.DTOs;
using RequestStockService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Third_Party_Stock_Request_xUnit_Tests
{
    public class ThirdPartyStockRepoTest
    {
        private Mock<HttpMessageHandler> CreateHttpMock(HttpStatusCode expectedCode,
                                                        string expectedJson)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = expectedCode
            };
            if (expectedJson != null)
            {
                response.Content = new StringContent(expectedJson,
                                                     Encoding.UTF8,
                                                     "application/json");
            }
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response)
                .Verifiable();
            return mock;
        }

        private IThirdPartyStockRepository ThirdPartyStockService(HttpClient client)
        {
            var mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
            mockConfiguration.Setup(c => c["THIRD_PARTY_BASE_URL"])
                             .Returns("http://undercutters.azurewebsites.net/api/");
            return new ThirdPartyStockRepository(mockConfiguration.Object, client);
        }

        [Fact]
        public async void GetAllProducts_True()
        {
            //Arrange
            var expectedResult = new ReadThirdPartyProductsDTO[]
            {
                new ReadThirdPartyProductsDTO() {Id = 1, Name = "Screen Protector"},
                new ReadThirdPartyProductsDTO() {Id = 2, Name = "Phone"}
            };
            var expectedJson = JsonConvert.SerializeObject(expectedResult);
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Product");
            var mock = CreateHttpMock(HttpStatusCode.OK, expectedJson);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = (await service.GetAllThirdPartyProducts()).ToArray();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Length, result.Length);
            for (int i = 0; i < result.Length; i++)
            {
                Assert.Equal(expectedResult[i].Id, result[i].Id);
            }

            mock.Protected()
                .Verify("SendAsync",
                        Times.Once(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        [Fact]
        public async void GetAllProducts_Empty()
        {
            //Arrange
            var expectedResult = new ReadThirdPartyProductsDTO[]
            {
            };
            var expectedJson = JsonConvert.SerializeObject(expectedResult);
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Product");
            var mock = CreateHttpMock(HttpStatusCode.OK, expectedJson);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = await service.GetAllThirdPartyProducts();

            //Assert
            int count = 0;
            Assert.Equal(result.Count(), count);

            mock.Protected()
                .Verify("SendAsync",
                        Times.Once(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        [Fact]
        public async void GetAllProducts_NotFound()
        {
            //Arrange
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Product");
            var mock = CreateHttpMock(HttpStatusCode.NotFound, null);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = await service.GetAllThirdPartyProducts();

            //Assert
            Assert.Null(result);

            mock.Protected()
                .Verify("SendAsync",
                        Times.Once(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        [Fact]
        public async void SendPurchaseRequest_True()
        {
            //Arrange
            var expectedResult = new PurchaseRequestSendDTO() { ProductId = 1 };
            var expectedJson = JsonConvert.SerializeObject(expectedResult);
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Order");
            var mock = CreateHttpMock(HttpStatusCode.OK, null);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = await service.SendPurchaseRequest(expectedResult);

            //Assert
            Assert.True(result);

            mock.Protected()
                .Verify("SendAsync",
                        Times.Once(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Post
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        [Fact]
        public async void SendPurchaseRequest_NotFound()
        {
            //Arrange
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Order");
            var mock = CreateHttpMock(HttpStatusCode.NotFound, null);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = await service.SendPurchaseRequest(null);

            //Assert
            Assert.False(result);

            mock.Protected()
                .Verify("SendAsync",
                        Times.Never(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        [Fact]
        public async void SendPurchaseRequest_Null()
        {
            //Arrange
            var expectedUri = new Uri("http://undercutters.azurewebsites.net/api/Order");
            var mock = CreateHttpMock(HttpStatusCode.OK, null);
            var client = new HttpClient(mock.Object);
            var service = ThirdPartyStockService(client);

            //Act
            var result = await service.SendPurchaseRequest(null);

            //Assert
            Assert.False(result);

            mock.Protected()
                .Verify("SendAsync",
                        Times.Never(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }
    }
}
