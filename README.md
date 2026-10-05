# Pipeline Pattern

A small .NET demo of the pipeline (chain of responsibility) pattern, using loan eligibility checks as the example.

## How it works

- Each check is an `IEligibilityRule` with an `Order` and an `Evaluate(EligibilityContext)` method.
- `EligibilityPipeline` runs the rules in `Order`, sharing one `EligibilityContext`.
- A rule can **reject** (the pipeline stops immediately) or add a **warning**.
- Final status:
  - no warnings → `Approved`
  - 1–2 warnings → `ApprovedWithConditions`
  - 3 or more warnings → `Rejected`
- Rules are registered with `Microsoft.Extensions.DependencyInjection`, so adding a rule means writing a class and one `AddTransient` line in `Program.cs`.

Rules: sanctioned country, age, minimum income, debt-to-income, loan amount, employment history.

## Run

```
dotnet run --project src/LoanPipeline
```

This evaluates four sample applications (approved, approved with conditions, rejected by rule, short-circuited by a sanctioned country) and prints the results.

## Layout

```
src/
  PipelinePattern.slnx
  LoanPipeline/
    Models/    LoanApplication, LoanDecision, LoanStatus
    Rules/     one class per eligibility rule
    EligibilityPipeline.cs
    Program.cs
```
