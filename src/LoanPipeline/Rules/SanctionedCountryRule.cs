namespace LoanPipeline.Rules;

public class SanctionedCountryRule : IEligibilityRule
{
    public int Order => 0; // First in chain to short-circuit invalid jurisdictions immediately

    private static readonly HashSet<string> Sanctioned = new(StringComparer.OrdinalIgnoreCase)
    {
        "IR", "KP", "SY", "CU"
    };

    public void Evaluate(EligibilityContext context)
    {
        if (Sanctioned.Contains(context.LoanApplication.Country))
            context.Reject("Applications from sanctioned countries are not accepted.");
    }
}