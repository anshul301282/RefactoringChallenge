using APIUnitTest.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RefactoringChallenge.Controllers;
using RefactoringChallenge.Mapper;
using RefactoringChallenge.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIUnitTest
{
    public class OrderUnitTest
    {
        private readonly OrdersController _ordersController;
        private readonly Mock<IOrderRepositoryAsync> _iOrderRepositoryMock = new Mock<IOrderRepositoryAsync>();
        private readonly Mock<IOrderDetailsRepositoryAsync> _iOrderDetailsRepositoryMock = new Mock<IOrderDetailsRepositoryAsync>();
        private readonly Mock<ILogger<OrdersController>> _loggerMock = new Mock<ILogger<OrdersController>>();
        
        public OrderUnitTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _ordersController = new OrdersController(mapper, _iOrderRepositoryMock.Object, _iOrderDetailsRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void Get_Orders()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrdersAsync())
                .ReturnsAsync(OrderMockData.GetOrderMockData());
            // Act
            var result = await _ordersController.Get();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<OrderResponse>>(okResult.Value);
        }
        [Fact]
        public async void Get_OrderById()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            // Act
            var result = await _ordersController.GetById(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsAssignableFrom<OrderResponse>(okResult.Value);
        }
        [Fact]
        public async void Get_OrderById_NotFound()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(2))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            // Act
            var result = await _ordersController.GetById(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId);
            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(404, okResult.StatusCode);
            
        }
        [Fact]
        public async void Delete_Order()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            _iOrderRepositoryMock
                .Setup(o => o.DeleteOrderAsync(OrderMockData.GetOrderMockData().FirstOrDefault()))
                .ReturnsAsync(true);
            // Act
            var result = await _ordersController.Delete(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async void Delete_Order_NotFound()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(2))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            _iOrderRepositoryMock
                .Setup(o => o.DeleteOrderAsync(OrderMockData.GetOrderMockData().FirstOrDefault()))
                .ReturnsAsync(true);
            // Act
            var result = await _ordersController.Delete(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId);
            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(404, okResult.StatusCode);
        }
        [Fact]
        public async void Add_Product()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            _iOrderDetailsRepositoryMock
                .Setup(o => o.AddProductsToOrderAsync(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId, OrderMockData.GetOrderDetailsMockData()).Result)
                .Returns(OrderMockData.GetOrderMockData().FirstOrDefault().OrderDetails.FirstOrDefault());

            // Act
            var result = await _ordersController.AddProductsToOrder(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId, OrderMockData.GetOrderDetailsMockData());
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async void Add_Product_NotFound()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.GetOrderByIdAsync(2))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            _iOrderDetailsRepositoryMock
                .Setup(o => o.AddProductsToOrderAsync(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId, OrderMockData.GetOrderDetailsMockData()).Result)
                .Returns(OrderMockData.GetOrderMockData().FirstOrDefault().OrderDetails.FirstOrDefault());

            // Act
            var result = await _ordersController.AddProductsToOrder(OrderMockData.GetOrderMockData().FirstOrDefault().OrderId, OrderMockData.GetOrderDetailsMockData());
            // Assert
            var okResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(404, okResult.StatusCode);
        }

        [Fact]
        public async void Create_Order()
        {
            // Arrange     
            _iOrderRepositoryMock
                .Setup(o => o.CreateOrderAsync(OrderMockData.GetOrderRequestMockData().FirstOrDefault()))
                .ReturnsAsync(OrderMockData.GetOrderMockData().FirstOrDefault());
            
            // Act
            var result = await _ordersController.Create(OrderMockData.GetOrderRequestMockData().FirstOrDefault());
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
