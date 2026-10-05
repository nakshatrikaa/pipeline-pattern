namespace LoanPipeline.Rules;

public class MinimumIncomeRule : IEligibilityRule
{
    public int Order => 20;

    public void Evaluate(EligibilityContext context)
    {
        if (context.LoanApplication.AnnualIncome < 20_000m)
            context.Reject("Annual income is below the minimum required threshold of $20,000.");
    }
}