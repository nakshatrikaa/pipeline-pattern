namespace LoanPipeline.Models;

public class LoanDecision
{
    public Guid ApplicationId { get; set; }
    
    public LoanStatus Status { get; init; }
    
    public string? RejectionReason { get; init; }
    
    public List<string> Warnings { get; init; } = [];
}