namespace LoanPipeline.Rules;

public class EmploymentRule : IEligibilityRule
{
    public int Order => 50;

    public void Evaluate(EligibilityContext context)
    {
        if (context.LoanApplication.EmploymentYears < 1)
            context.Warn("Applicant has less than 1 full year of continuous employment.");
    }
}