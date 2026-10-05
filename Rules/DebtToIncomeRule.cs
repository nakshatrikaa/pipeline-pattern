namespace LoanPipeline.Rules;

public class DebtToIncomeRule : IEligibilityRule
{
    public int Order => 30;

    public void Evaluate(EligibilityContext context)
    {
        var dti = context.LoanApplication.DebtToIncomeRatio;

        if (dti > 0.50m)
        {
            context.Reject("Debt-to-income ratio exceeds the absolute limit of 50%.");
            return;
        }

        if (dti > 0.40m) context.Warn("Debt-to-income ratio is elevated (greater than 40%).");
    }
}