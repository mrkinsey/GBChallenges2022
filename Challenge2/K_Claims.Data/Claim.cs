
using System;
public class Claim
{
    public Claim() { }

    public Claim(int claimId, ClaimType claimType, string description, double claimAmount, DateTime dateOfIncident, DateTime dateOfClaim, bool isValid)
    {
        ClaimId = claimId;
        ClaimType = claimType;
        Description = description;
        ClaimAmount = claimAmount;
        DateOfIncident = dateOfIncident;
        DateOfClaim = dateOfClaim;
        IsValid = isValid;
    }
    public int ClaimId { get; set; }
    public ClaimType ClaimType { get; set; }
    public string Description { get; set; }
    public double ClaimAmount { get; set; }
    public DateTime DateOfIncident { get; set; }
    public DateTime DateOfClaim { get; set; }
    public bool IsValid { get; set; }
}
