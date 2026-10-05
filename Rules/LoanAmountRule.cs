namespace LoanPipeline.Rules;

public class LoanAmountRule : IEligibilityRule
{
    public int Order => 40;

    public void Evaluate(EligibilityContext context)
    {
        var amount = context.LoanApplication.RequestedAmount;

        if (amount < 1_000m)
        {
            context.Reject("Requested loan amount is under the $1,000 minimum threshold.");
            return;
        }

        if (amount > 100_000m)
            context.Warn("Requested loan amount exceeds $100,000 and requires senior underwriter sign-off.");
    }
}