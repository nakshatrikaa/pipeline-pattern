namespace LoanPipeline;

public interface IEligibilityRule
{
    int Order { get; }
    
    void Evaluate(EligibilityContext context);
}