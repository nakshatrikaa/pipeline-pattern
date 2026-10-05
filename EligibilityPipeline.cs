using LoanPipeline.Models;

namespace LoanPipeline;

public class EligibilityPipeline
{
    private readonly List<IEligibilityRule> _rules;

    public EligibilityPipeline(IEnumerable<IEligibilityRule> rules)
    {
        // Executes rules in strictly determined order
        _rules = [.. rules.OrderBy(r => r.Order)];
    }

    public LoanDecision Run(LoanApplication application)
    {
        var context = new EligibilityContext(application);

        foreach (var rule in _rules)
        {
            rule.Evaluate(context);

            // Short-circuit pipeline on hard rejection
            if (context.IsRejected)
                return new LoanDecision
                {
                    ApplicationId = application.Id,
                    Status = LoanStatus.Rejected,
                    RejectionReason = context.RejectionReason ?? "Application rejected."
                };
        }

        return context.Warnings.Count switch
        {
            0 => new LoanDecision { ApplicationId = application.Id, Status = LoanStatus.Approved },
            <= 2 => new LoanDecision
            {
                ApplicationId = application.Id,
                Status = LoanStatus.ApprovedWithConditions,
                Warnings = context.Warnings
            },
            _ => new LoanDecision
            {
                ApplicationId = application.Id,
                Status = LoanStatus.Rejected,
                RejectionReason = "Application exceeded maximum allowable warnings."
            }
        };
    }
}