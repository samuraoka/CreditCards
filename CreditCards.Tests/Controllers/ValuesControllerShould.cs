using CreditCards.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace CreditCards.Tests.Controllers
{
    public class ValuesControllerShould
    {
        [Fact]
        public void ReturnValues()
        {
            // Arrange
            var sut = new ValuesController();

            // Act
            var result = sut.Get().ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal("value1", result[0]);
            Assert.Equal("value2", result[1]);
        }

        [Fact]
        public void ReturnBadRequest()
        {
            // Arrange
            var sut = new ValuesController();

            // Act
            var result = sut.Get(0);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid request for id 0", badRequestResult.Value);
        }

        [Fact]
        public void StartJob()
        {
            // Arrange
            var sut = new ValuesController();

            // Act
            var result = sut.StartJob();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Batch Job Started", okResult.Value);
        }
    }
}
