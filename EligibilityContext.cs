using LoanPipeline.Models;

namespace LoanPipeline;

public class EligibilityContext(LoanApplication loanApplication)
{
    public LoanApplication LoanApplication { get; } = loanApplication;
    public List<string> Warnings { get; } = [];
    public bool IsRejected { get; private set; }
    public string? RejectionReason { get; private set; }

    public void Reject(string reason)
    {
        IsRejected = true;
        RejectionReason = reason;
    }

    public void Warn(string reason)
    {
        Warnings.Add(reason);
    }
}