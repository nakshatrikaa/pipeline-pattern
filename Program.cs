using LoanPipeline;
using LoanPipeline.Models;
using LoanPipeline.Rules;
using Microsoft.Extensions.DependencyInjection;

// 1. Setup Dependency Injection and Run
var services = new ServiceCollection();

// Register the individual rules
services.AddTransient<IEligibilityRule, SanctionedCountryRule>();
services.AddTransient<IEligibilityRule, AgeRule>();
services.AddTransient<IEligibilityRule, MinimumIncomeRule>();
services.AddTransient<IEligibilityRule, DebtToIncomeRule>();
services.AddTransient<IEligibilityRule, LoanAmountRule>();
services.AddTransient<IEligibilityRule, EmploymentRule>();

// Register the pipeline
services.AddTransient<EligibilityPipeline>();

var provider = services.BuildServiceProvider();
var pipeline = provider.GetRequiredService<EligibilityPipeline>();

// 2. Run Test Applications
var testCases = new List<LoanApplication>
{
    // Case 1: Fully Approved
    new()
    {
        Age = 35,
        AnnualIncome = 85_000m,
        DebtToIncomeRatio = 0.25m,
        RequestedAmount = 20_000m,
        EmploymentYears = 5,
        Country = "US"
    },

    // Case 2: Approved With Conditions (1 or 2 warnings)
    new()
    {
        Age = 67, // Warning: over 65
        AnnualIncome = 45_000m,
        DebtToIncomeRatio = 0.42m, // Warning: DTI > 40%
        RequestedAmount = 15_000m,
        EmploymentYears = 4,
        Country = "CA"
    },

    // Case 3: Rejected by Rule (Income under $20,000)
    new()
    {
        Age = 25,
        AnnualIncome = 15_000m,
        DebtToIncomeRatio = 0.30m,
        RequestedAmount = 5_000m,
        EmploymentYears = 2,
        Country = "GB"
    },

    // Case 4: Short-circuited immediately (Sanctioned country)
    new()
    {
        Age = 40,
        AnnualIncome = 120_000m,
        DebtToIncomeRatio = 0.15m,
        RequestedAmount = 50_000m,
        EmploymentYears = 10,
        Country = "IR"
    }
};

Console.WriteLine("================ LOAN PIPELINE EVALUATION ================\n");

for (var i = 0; i < testCases.Count; i++)
{
    var app = testCases[i];
    var decision = pipeline.Run(app);

    Console.WriteLine($"Application #{i + 1} ({app.Country}, Age: {app.Age}, Income: ${app.AnnualIncome:N0}):");
    Console.WriteLine($"  Status: {decision.Status}");

    if (decision.Status == LoanStatus.Rejected) Console.WriteLine($"  Rejection Reason: {decision.RejectionReason}");

    if (decision.Warnings.Any())
    {
        Console.WriteLine("  Warnings:");
        foreach (var warning in decision.Warnings) Console.WriteLine($"    - {warning}");
    }

    Console.WriteLine();
}