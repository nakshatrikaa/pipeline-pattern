namespace LoanPipeline.Rules;

public class AgeRule : IEligibilityRule
{
    public int Order => 10;

    public void Evaluate(EligibilityContext context)
    {
        var app = context.LoanApplication;

        if (app.Age < 18)
        {
            context.Reject("Applicant must be at least 18 years old.");
            return;
        }

        if (app.Age > 65) context.Warn("Applicant is over 65 years old.");
    }
}