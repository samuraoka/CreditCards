using CreditCards.Core.Interface;
using System.Runtime.CompilerServices;

// InternalsVisibleToAttribute Class
// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.internalsvisibletoattribute?view=netframework-4.7.1
[assembly: InternalsVisibleTo("CreditCards.Tests")]

namespace CreditCards.Core.Model
{
    public class CreditCardApplicationEvaluator
    {
        private readonly IFrequentFlyerNumberValidator _validator;

        internal const int AutoReferralMaxAge = 20;
        internal const int LowIncomeThreshold = 20_000;
        internal const int HighIncomeThreshold = 100_000;

        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator validator)
        {
            _validator = validator;
        }

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            if (!_validator.IsValid(application.FrequentFlyerNumber))
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome >= LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            return CreditCardApplicationDecision.AutoDeclined;
        }
    }
}
