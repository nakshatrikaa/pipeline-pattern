namespace LoanPipeline.Models;

public class LoanApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public int Age { get; init; }
    
    public decimal AnnualIncome { get; init; }
    
    public decimal DebtToIncomeRatio { get; init; }
    
    public decimal RequestedAmount { get; init; }
    
    public int EmploymentYears { get; init; }
    
    public string Country { get; init; } = string.Empty;
}