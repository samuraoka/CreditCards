namespace CreditCards.Core.Model
{
    public class CreditCardApplicationEvaluator
    {
        private const int AutoReferralMaxAge = 20;
        public const int LowIncomeThreshold = 20_000;
        public const int HighIncomeThreshold = 100_000;

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
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
