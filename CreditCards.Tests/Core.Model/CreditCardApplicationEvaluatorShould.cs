using CreditCards.Core.Interface;
using Moq;
using Xunit;

namespace CreditCards.Core.Model.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        private const int ExpectedLowIncomeThreshold
            = CreditCardApplicationEvaluator.LowIncomeThreshold;
        private const int ExpectedHighIncomeThreshold
            = CreditCardApplicationEvaluator.HighIncomeThreshold;

        // Moq
        // Install-Package -Id Moq -ProjectName CreditCards.Tests
        // https://www.nuget.org/packages/Moq/
        private readonly Mock<IFrequentFlyerNumberValidator> _mockValidator;
        private readonly CreditCardApplicationEvaluator _sut; // sut means System Unter Test

        public CreditCardApplicationEvaluatorShould()
        {
            // Moq: Quickstart
            // https://github.com/Moq/moq4/wiki/Quickstart
            _mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            _sut = new CreditCardApplicationEvaluator(_mockValidator.Object);
        }

        [Theory]
        [InlineData(ExpectedHighIncomeThreshold)]
        [InlineData(ExpectedHighIncomeThreshold + 1)]
        [InlineData(int.MaxValue)]
        public void AcceptAllHighIncomeApplicants(int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
            };

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted,
                _sut.Evaluate(application));
        }

        [Theory]
        [InlineData(20, ExpectedHighIncomeThreshold - 1)]
        [InlineData(19, ExpectedHighIncomeThreshold - 1)]
        [InlineData(0, ExpectedHighIncomeThreshold - 1)]
        [InlineData(int.MinValue, ExpectedHighIncomeThreshold - 1)]
        public void ReferYoungApplicantsWhoAreNotHighIncome(int age, int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = age,
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));
        }

        [Theory]
        [InlineData(ExpectedLowIncomeThreshold, 21)]
        [InlineData(ExpectedLowIncomeThreshold + 1, 21)]
        [InlineData(ExpectedHighIncomeThreshold - 1, 21)]
        public void ReferNonYoungApplicantsWhoAreMiddleIncome(int income, int age)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = age,
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));
        }

        [Theory]
        [InlineData(ExpectedLowIncomeThreshold - 1)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void DeclineAllApplicantsWhoAreLowIncome(int income)
        {
            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = 21,
            };

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined,
                _sut.Evaluate(application));
        }

        [Fact]
        public void ReferInvalidFrequentFlyerNumbers()
        {
            // this statement overrides default behavior of this mock object that is set up in this constructor
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            var application = new CreditCardApplication();

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                _sut.Evaluate(application));

            // Moq: Verification
            // https://github.com/Moq/moq4/wiki/Quickstart#verification
            _mockValidator.Verify(x => x.IsValid(It.IsAny<string>()), Times.Once);
        }
    }
}
