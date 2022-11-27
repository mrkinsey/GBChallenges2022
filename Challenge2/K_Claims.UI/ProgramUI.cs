
public class ProgramUI
{
    private ClaimRepository _claimRepo = new ClaimRepository();
    public void Run()
    {
        SeedContent();
        RunApplication();
    }

    bool isRunning = true;
    public void RunApplication()
    {
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Komodo Claims Department\n" +
                "1. View all claims\n" +
                "2. Add a new claim\n" +
                "3. Process next claim\n" +
                "4. Exit application");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    ViewAllClaims();
                    break;
                case "2":
                    AddNewClaim();
                    break;
                case "3":
                    ProcessNextClaim();
                    break;
                case "4":
                    ExitApplication();
                    break;
            }
        }
    }
    public void ViewAllClaims()
    {
        Console.Clear();
        Queue<Claim> claimList = _claimRepo.ViewAllClaims();

        if (claimList.Count > 0)
        {
            foreach (var claim in claimList)
            {
                ViewClaim(claim);
            }
        }
        else
        {
            System.Console.WriteLine("Sorry, there are no claims in the database.\n" +
                                    "-------------------------\n" +
                                    "Press enter to continue.");
        }
        Console.ReadKey();
    }

    private void ViewClaim(Claim claim)
    {
        System.Console.WriteLine($"ClaimID: {claim.ClaimId}\n" +
                                $"Claim Type: {claim.ClaimType}\n" +
                                $"Description {claim.Description}\n" +
                                $"Claim Amount: {claim.ClaimAmount}\n" +
                                $"Date of Incident: {claim.DateOfIncident}\n" +
                                $"Date Of Claim: {claim.DateOfClaim}\n" +
                                $"Valid: {claim.IsValid}\n");
    }

    public void AddNewClaim()
    {
        try
        {
            Claim claim = new Claim();

            Console.Clear();
            Console.WriteLine("Enter the type of claim:\n" +
                "1. Car\n" +
                "2. Home\n" +
                "3. Theft\n");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    claim.ClaimType = ClaimType.car;
                    break;
                case "2":
                    claim.ClaimType = ClaimType.home;
                    break;
                case "3":
                    claim.ClaimType = ClaimType.theft;
                    break;
            }

            Console.WriteLine("Enter a description of the claim ");
            claim.Description = Console.ReadLine();

            Console.WriteLine("Enter the claim amount:");
            claim.ClaimAmount = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the date of the incident: ");
            claim.DateOfIncident = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter the date of claim: ");
            claim.DateOfClaim = DateTime.Parse(Console.ReadLine());

            _claimRepo.IsValid(claim);

            Console.Clear();
            Console.WriteLine($"You are about to add the following claim to the queue: \n" +
                $"\n" +
                $"ID: {claim.ClaimId}\n" +
                $"Type: {claim.ClaimType}\n" +
                $"Description: {claim.Description}\n" +
                $"Amount: ${claim.ClaimAmount}\n" +
                $"Incident Date: {claim.DateOfIncident}\n" +
                $"Claim Date: {claim.DateOfClaim}\n" +
                $"Valid: {claim.IsValid}\n" +
                $"\n" +
                $"Press any key to confirm the entry...");
            Console.ReadKey();

            _claimRepo.AddNewClaim(claim);

            Console.Clear();
            Console.WriteLine("Claim successfully added to the queue!\n" +
                "Press any key to return to the menu...");
            Console.ReadKey();
        }
        catch
        {
            System.Console.WriteLine("Sorry, invalid entry, please try again.");
        }
    }

    public void ProcessNextClaim()
    {
        Console.Clear();
        Console.WriteLine("Here are the details on the next claim to be handled: \n");

        Queue<Claim> newList = _claimRepo.ViewAllClaims();
        Claim nextClaim = newList.Peek();

        Console.WriteLine($"Claim ID: {nextClaim.ClaimId}\n" +
            $"Type: {nextClaim.ClaimType}\n" +
            $"Description: {nextClaim.Description}\n" +
            $"Amount: ${nextClaim.ClaimAmount}\n" +
            $"Incident Date: {nextClaim.DateOfIncident.ToShortDateString()}\n" +
            $"Claim Date: {nextClaim.DateOfClaim.ToShortDateString()}\n" +
            $"Valid: {nextClaim.IsValid}\n" +
            $"\n" +
            $"Do you want to take this claim? (y/n)");

        string userInput = Console.ReadLine();
        switch (userInput)
        {
            case "y":
                newList.Dequeue();
                Console.WriteLine("You have successfully taken the claim\n" +
                    "Press any key to continue...");
                break;
            case "n":
                RunApplication();
                break;
            default:
                Console.WriteLine("Please enter either y or n");
                break;
        }
        Console.ReadLine();
    }

    public void ExitApplication()
    {
        isRunning = false;
    }

    public void SeedContent()
    {
        Claim claimOne = new Claim(1, ClaimType.car, "One car accident: driver lost control on ice patch", 750, DateTime.Parse("11/18/2022"), DateTime.Parse("11/22/2022"), true);
        Claim claimTwo = new Claim(2, ClaimType.home, "Roof damage from hail", 14750.59d, DateTime.Parse("9/21/2022"), DateTime.Parse("11/17/2022"), false);
        Claim claimThree = new Claim(3, ClaimType.theft, "Precious diamond stolen", 150000, DateTime.Parse("8/24/2022"), DateTime.Parse("8/27/2022"), true);

        _claimRepo.AddNewClaim(claimOne);
        _claimRepo.AddNewClaim(claimTwo);
        _claimRepo.AddNewClaim(claimThree);
    }
}