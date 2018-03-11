using CreditCards.Core.Interface;
using CreditCards.Web.Controllers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CreditCards.Tests.Controllers
{
    public class ApplyControllerShould
    {
        private readonly Mock<ICreditCardApplicationRepository> mockRepository;
        private readonly Mock<IDataProtectionProvider> mockDataProtectionProvider;
        private readonly ApplyController sut;

        public ApplyControllerShould()
        {
            // Arrange
            mockRepository = new Mock<ICreditCardApplicationRepository>();
            mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
            sut = new ApplyController(mockRepository.Object, mockDataProtectionProvider.Object);
        }

        [Fact]
        public void ReturnViewForIndex()
        {
            // Act
            IActionResult result = sut.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
