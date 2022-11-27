
using System;
public class ClaimRepository
{
    private Queue<Claim> _claimRepo = new Queue<Claim>();
    private int _count;
    public Queue<Claim> ViewAllClaims()
    {
        return _claimRepo;
    }

    public bool AddNewClaim(Claim newClaim)
    {
        int startingCount = _claimRepo.Count;
        _claimRepo.Enqueue(newClaim);

        if (_claimRepo.Count > startingCount)
        {
            AssignClaimId(newClaim);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveClaim()
    {
        _claimRepo.Dequeue();
    }

    public void IsValid(Claim claim)
    {
        if (claim.DateOfClaim < claim.DateOfIncident)
            claim.DateOfClaim = claim.DateOfIncident;

        TimeSpan validClaim = claim.DateOfClaim - claim.DateOfIncident;

        if (validClaim.Days <= 30)
        {
            claim.IsValid = true;
        }
        else
            claim.IsValid = false;
    }

    private void AssignClaimId(Claim claim)
    {
        _count++;
        claim.ClaimId = _count;
    }
}
