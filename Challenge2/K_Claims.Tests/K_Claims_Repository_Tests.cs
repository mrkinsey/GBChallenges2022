using System.Xml.Serialization;
using System.ComponentModel;
namespace K_Claims.Tests;
using Xunit;

public class K_Claims_Repository_Tests
{
    private ClaimRepository _globalRepo;
    private Claim _claimA;
    private Claim _claimB;
    private Claim _claimC;

    public K_Claims_Repository_Tests()
    {
        _globalRepo = new ClaimRepository();
        _claimA = new Claim(1, ClaimType.car, "One car accident: driver lost control on ice patch", 750, DateTime.Parse("11/18/2022"), DateTime.Parse("11/22/2022"), true);
        _claimB = new Claim(2, ClaimType.home, "Roof damage from hail", 14750.59d, DateTime.Parse("9/21/2022"), DateTime.Parse("11/17/2022"), false);
        _claimC = new Claim(3, ClaimType.theft, "Precious diamond stolen", 150000, DateTime.Parse("8/24/2022"), DateTime.Parse("8/27/2022"), true);

        _globalRepo.AddNewClaim(_claimA);
        _globalRepo.AddNewClaim(_claimB);
        _globalRepo.AddNewClaim(_claimC);
    }

    [Fact]
    public void NewClaim_ShouldReturnBoolean()
    {
        Claim item = new Claim();
        ClaimRepository repository = new ClaimRepository();

        bool addResult = repository.AddNewClaim(item);

        Assert.True(addResult);
    }

    [Fact]
    public void Get_DatabaseInfo_Should_Return_CorrectCollection()
    {
        Claim content = new Claim();
        ClaimRepository repository = new ClaimRepository();

        repository.AddNewClaim(content);

        Queue<Claim> contents = repository.ViewAllClaims();

        bool databaseHasContents = contents.Contains(content);

        Assert.True(databaseHasContents);
    }

    [Fact]
    public void Valid_Test_Should_Return_True()
    {
        bool expected = true;
        bool actual = _claimA.IsValid;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Valid_Test_Should_Return_False()
    {
        bool expected = false;
        bool actual = _claimB.IsValid;

        Assert.Equal(expected, actual);
    }
}