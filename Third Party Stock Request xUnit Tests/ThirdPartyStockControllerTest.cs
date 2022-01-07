using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RequestStockService.Controllers;
using RequestStockService.DTOs;
using RequestStockService.Profiles;
using RequestStockService.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Third_Party_Stock_Request_xUnit_Tests
{
    public class ThirdPartyStockControllerTest
    {
        private ReadThirdPartyProductsDTO readThirdPartProductsDTO;
        private PurchaseRequestSendDTO sendPurchaseRequestDTO;
        private List<PurchaseRequestSendDTO> listSendPurchaseRequestDTO;
        private List<ReadThirdPartyProductsDTO> readListThirdPartyProductsDTO;
        private FakePurchaseRequestRepository fakeRepo;
        private Mock<IPurchaseRequestRepository> mockRepo;
        private IMapper mapper;
        private ILogger<PurchaseRequestController> logger;
        private PurchaseRequestController controller;

        private void SetUpController(PurchaseRequestController controller)
        {
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        public void SetUpSendPurchaseRequestDTO()
        {
            sendPurchaseRequestDTO = new PurchaseRequestSendDTO()
            {
                ProductId = 1,
                AccountName = "Jacob",
                CardNumber = "1111222233334444",
                Quantity = 10
            };
        }

        public void SetUpFakeReadProductList()
        {
            readListThirdPartyProductsDTO = new List<ReadThirdPartyProductsDTO>()
            {
                new ReadThirdPartyProductsDTO(){Id = 1},
                new ReadThirdPartyProductsDTO(){Id = 2}
            };
        }

        private void SetFakeRepo()
        {
            fakeRepo = new FakePurchaseRequestRepository
            {
                _readProductList = readListThirdPartyProductsDTO,
                _purchaseList = listSendPurchaseRequestDTO
            };
        }

        private void SetMapper()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PurchaseRequestProfile());
            }).CreateMapper();
        }

        private void SetLogger()
        {
            logger = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<PurchaseRequestController>();
        }

        private void SetMockReviewRepo()
        {
            mockRepo = new Mock<IPurchaseRequestRepository>();

            mockRepo.Setup(repo => repo.GetAllThirdPartyProducts())
              .ReturnsAsync(new List<ReadThirdPartyProductsDTO>()).Verifiable();

            mockRepo.Setup(repo => repo.SendPurchaseRequest(It.IsAny<PurchaseRequestSendDTO>())).Verifiable();
        }

        private void DefaultSetup()
        {
            SetUpSendPurchaseRequestDTO();
            SetUpFakeReadProductList();
            SetMapper();
        }

        private void SetupWithFakes()
        {
            DefaultSetup();
            SetFakeRepo();
            controller = new PurchaseRequestController(fakeRepo, mapper, logger);
            SetUpController(controller);
        }

        private void SetupWithMocks()
        {
            DefaultSetup();
            SetMockReviewRepo();
            controller = new PurchaseRequestController(mockRepo.Object, mapper, logger);
            SetUpController(controller);
        }

        #region Testing With Fakes
        [Fact]
        public async void GetThirdPartProducts_NotNull()
        {
            //Arange
            SetupWithFakes();

            //Act
            var result = await controller.GetThirdPartyProducts();

            //Arrange
            Assert.NotNull(result);
        }

        [Fact]
        public async void SendPurchaseRequest_NotNull()
        {
            //Arange
            SetupWithFakes();
            
            //Act
            var result = await controller.SendPurchaseRequest(sendPurchaseRequestDTO);

            //Arrange
            Assert.NotNull(result);
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);
        }

        [Fact]
        public async void SendNullRequest_Null()
        {
            //Arange
            SetupWithFakes();

            //Act
            var result = await controller.SendPurchaseRequest(null);

            //Arrange
            Assert.NotNull(result);
            var objResult = result as BadRequestObjectResult;
            Assert.Null(objResult);
        }
        #endregion

        #region Test With Mocks
        [Fact]
        public async void GetAllProducts_True_Mock()
        {
            //Arrange
            SetupWithMocks();
            
            //Act
            var result = await controller.GetThirdPartyProducts();

            //Assert
            Assert.NotNull(result);
            mockRepo.Verify(x => x.GetAllThirdPartyProducts(), Times.Once);
            mockRepo.Verify(x => x.SendPurchaseRequest(It.IsAny<PurchaseRequestSendDTO>()), Times.Never);
        }

        [Fact]
        public async void SendPurchaseRequest_True_Mock()
        {
            //Arrange
            SetupWithMocks();

            //Act
            var result = await controller.SendPurchaseRequest(sendPurchaseRequestDTO);

            //Assert
            Assert.NotNull(result);
            var objResult = result as OkObjectResult;
            Assert.NotNull(objResult);
            mockRepo.Verify(x => x.GetAllThirdPartyProducts(), Times.Never);
            mockRepo.Verify(x => x.SendPurchaseRequest(It.IsAny<PurchaseRequestSendDTO>()), Times.Once);
        }

        [Fact]
        public async void SendPurchaseRequestNull_Mock()
        {
            //Arrange
            SetupWithMocks();

            //Act
            var result = await controller.SendPurchaseRequest(null);

            //Assert
            Assert.NotNull(result);
            var objResult = result as BadRequestResult;
            Assert.Null(objResult);
            mockRepo.Verify(x => x.GetAllThirdPartyProducts(), Times.Never);
            mockRepo.Verify(x => x.SendPurchaseRequest(It.IsAny<PurchaseRequestSendDTO>()), Times.Never);
        }
        #endregion
    }
}
