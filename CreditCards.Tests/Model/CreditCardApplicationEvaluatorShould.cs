using CreditCards.Core.Model;
using Xunit;

namespace CreditCards.Tests.Model
{
    public class CreditCardApplicationEvaluatorShould
    {
        //TODO
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

        //TODO
    }
}
