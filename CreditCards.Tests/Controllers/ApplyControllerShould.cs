using CreditCards.Core.Interface;
using CreditCards.Core.Model;
using CreditCards.Web.Controllers;
using CreditCards.Web.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Moq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CreditCards.Tests.Controllers
{
    public class ApplyControllerShould
    {
        private readonly Mock<ICreditCardApplicationRepository> mockRepository;
        private readonly Mock<IDataProtectionProvider> mockDataProtectionProvider;
        private readonly Mock<IDataProtector> mockDataProtector;

        private readonly ApplyController sut;

        public ApplyControllerShould()
        {
            // Arrange
            mockDataProtector = new Mock<IDataProtector>();
            mockDataProtector.Setup(dp => dp.Protect(It.IsAny<byte[]>())).Returns(Encoding.ASCII.GetBytes("DummyIdString"));
            mockDataProtectionProvider = new Mock<IDataProtectionProvider>();
            mockDataProtectionProvider.Setup(dpp => dpp.CreateProtector(It.IsAny<string>())).Returns(mockDataProtector.Object);
            mockRepository = new Mock<ICreditCardApplicationRepository>();
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

        [Fact]
        public async void ReturnViewWhenInvalidModelState()
        {
            // Arrange
            sut.ModelState.AddModelError("x", "Test Error");
            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah",
            };

            // Act
            var result = await sut.Index(application);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<NewCreditCardApplicationDetails>(viewResult.Model);
            Assert.Equal(application.FirstName, model.FirstName);
        }

        [Fact]
        public async void NotSaveApplicationWhenModelError()
        {
            // Arrange
            sut.ModelState.AddModelError("x", "Test Error");
            var application = new NewCreditCardApplicationDetails();

            // Act
            await sut.Index(application);

            // Assert
            mockRepository.Verify(r => r.AddAsync(It.IsAny<CreditCardApplication>()), Times.Never);
        }

        [Fact]
        public async void SaveApplicationWhenValidModel()
        {
            // Arrange
            CreditCardApplication savedApplication = null;
            mockRepository.Setup(r => r.AddAsync(It.IsAny<CreditCardApplication>()))
                .Returns(Task.CompletedTask)
                .Callback<CreditCardApplication>(x => savedApplication = x);
            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah",
                LastName = "Smith",
                Age = 18,
                FrequentFlyerNumber = "012345-A",
                GrossAnnualIncome = 100_000,
            };

            // Act
            await sut.Index(application);

            // Assert
            mockRepository.Verify(r => r.AddAsync(It.IsAny<CreditCardApplication>()), Times.Once);

            Assert.Equal(application.FirstName, savedApplication.FirstName);
            Assert.Equal(application.LastName, savedApplication.LastName);
            Assert.Equal(application.Age, savedApplication.Age);
            Assert.Equal(application.FrequentFlyerNumber, savedApplication.FrequentFlyerNumber);
            Assert.Equal(application.GrossAnnualIncome, savedApplication.GrossAnnualIncome);
        }

        [Fact]
        public async void ReturnApplicationCompleteViewWhenValidModel()
        {
            // Arrange
            var application = new NewCreditCardApplicationDetails
            {
                FirstName = "Sarah",
                LastName = "Smith",
                Age = 18,
                FrequentFlyerNumber = "012345-A",
                GrossAnnualIncome = 100_000,
            };

            // Act
            var result = await sut.Index(application);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ApplyController.ApplicationComplete), redirectToAction.ActionName);
            Assert.Equal(WebEncoders.Base64UrlEncode(Encoding.ASCII.GetBytes("DummyIdString")), redirectToAction.RouteValues["id"]);
        }
    }
}
