using CreditCards.Core.Model;
using Xunit;

namespace CreditCards.Tests.Model
{
    public class CreditCardApplicationEvaluatorShould
    {
        private const int ExpectedLowIncomeThreshold
            = CreditCardApplicationEvaluator.LowIncomeThreshold;
        private const int ExpectedHighIncomeThreshold
            = CreditCardApplicationEvaluator.HighIncomeThreshold;

        [Theory]
        [InlineData(ExpectedHighIncomeThreshold)]
        [InlineData(ExpectedHighIncomeThreshold + 1)]
        [InlineData(int.MaxValue)]
        public void AcceptAllHighIncomeApplicants(int income)
        {
            var sut = new CreditCardApplicationEvaluator();

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income
            };

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted,
                sut.Evaluate(application));
        }

        [Theory]
        [InlineData(20, ExpectedHighIncomeThreshold - 1)]
        [InlineData(19, ExpectedHighIncomeThreshold - 1)]
        [InlineData(0, ExpectedHighIncomeThreshold - 1)]
        [InlineData(int.MinValue, ExpectedHighIncomeThreshold - 1)]
        public void ReferYoungApplicantsWhoAreNotHighIncome(int age, int income)
        {
            var sut = new CreditCardApplicationEvaluator();

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = age,
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                sut.Evaluate(application));
        }

        [Theory]
        [InlineData(ExpectedLowIncomeThreshold, 21)]
        [InlineData(ExpectedLowIncomeThreshold + 1, 21)]
        [InlineData(ExpectedHighIncomeThreshold - 1, 21)]
        public void ReferNonYoungApplicantsWhoAreMiddleIncome(int income, int age)
        {
            var sut = new CreditCardApplicationEvaluator();

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = age,
            };

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman,
                sut.Evaluate(application));
        }

        [Theory]
        [InlineData(ExpectedLowIncomeThreshold - 1)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public void DeclineAllApplicantsWhoAreLowIncome(int income)
        {
            var sut = new CreditCardApplicationEvaluator();

            var application = new CreditCardApplication
            {
                GrossAnnualIncome = income,
                Age = 21,
            };

            Assert.Equal(CreditCardApplicationDecision.AutoDeclined,
                sut.Evaluate(application));
        }
    }
}
