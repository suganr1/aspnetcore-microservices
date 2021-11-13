using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using FluentAssertions;
using Stock.API.Controllers;
using Stock.API.Repositories;
using Stock.API.Entities;

namespace Stock.xUnitTests
{
    public class StockControllerTests
    {
        private readonly Mock<IStockRepository> repositoryStub = new();
        private readonly Mock<ILogger<StockController>> loggerStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetStocksByDate_WithoutStock_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetStocksByDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync((IEnumerable<Stocks>)null);

            var controller = new StockController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetStocksByDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            // Assert
            //Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetStocksByDate_WithExistingStock_ReturnsExpectedItem()
        {
            // Arrange
            IEnumerable<Stocks> expectedItem = new Stocks[] { CreateRandomStock() };
            repositoryStub.Setup(repo => repo.GetStocksByDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedItem);

            var controller = new StockController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualItems = await controller.GetStocksByDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            // Assert
            var result = Assert.IsAssignableFrom<OkObjectResult>(actualItems.Result);
            result.Value.Should().BeEquivalentTo(expectedItem);
            //result.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        public async Task GetStocks_WithoutStock_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetStocks()).ReturnsAsync((IEnumerable<Stocks>)null);
            var controller = new StockController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetStocks();

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetStocks_WithExistingStock_ReturnsAllItems()
        {
            // Arrange
            IEnumerable<Stocks> expectedItems = new Stocks[] { CreateRandomStock(), CreateRandomStock() };

            repositoryStub.Setup(repo => repo.GetStocks())
                .ReturnsAsync(expectedItems);

            var controller = new StockController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualItems = await controller.GetStocks();

            // Assert
            var result = Assert.IsAssignableFrom<OkObjectResult>(actualItems.Result);
            result.Value.Should().BeEquivalentTo(expectedItems);
        }
        [Fact]
        public async Task AddStockAsync_WithStockToCreate_ReturnsCreatedStock()
        {
            // Arrange
            var itemToCreate = CreateRandomStock();

            var controller = new StockController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.AddStock(itemToCreate);

            // Assert
            var result1 = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            result1.Value.Should().BeEquivalentTo(itemToCreate.Id);
        }

        private Stocks CreateRandomStock()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyCode = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                CreatedBy = Guid.NewGuid().ToString(),
                CreatedDate = It.IsAny<DateTime>()
            };
        }
    }
}
