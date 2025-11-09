namespace Internship_2_C_Sharp;

public class User
{
    public int Id { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public DateTime DOB { get; set; }
    public List<Trip> Trips { get; set; }
        
}

public class Trip
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Distance { get; set; }
    public decimal FuelConsumed { get; set; }
    public decimal FuelCost { get; set; }
    public decimal TotalCost => FuelConsumed * FuelCost;
}


public static class Program
{
    private static List<User> users = new List<User>();
    private static int nextUserId = 0;
    private static int nextTripId = 0;
    public static void Main()
    {
        
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("1 - Korisnici");
            Console.WriteLine("2 - Putovanja");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            switch (Console.ReadLine())
            {
                case "1":
                    UsersMenu();
                    continue;
                case "2":
                    TripsMenu();
                    continue;
                case "0":
                    return;
            }
            
            Console.WriteLine("Neispravan unos, pokusajte ponovo!");
        }
    }

    private static void UsersMenu()
    {
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("1 - Unos novog korisnika");
            Console.WriteLine("2 - Brisanje korisnika");
            Console.WriteLine("3 - Uredjivanje korisnika");
            Console.WriteLine("4 - Pregled svih korisnika");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            switch (Console.ReadLine())
            {
                case "1":
                    AddUser();
                    continue;
                case "2":
                    DeleteUser();
                    continue;
                case "3":
                    EditUser();
                    continue;
                case "4":
                    ListUsers();
                    continue;
                case "0":
                    return;
            }
            
            Console.WriteLine("Neispravan unos, pokusajte ponovo!");
        }
    }
    
    private static void AddUser()
    {
        Console.Clear();
        Console.WriteLine("Stub - Add User");
    }
    
    private static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("Stub - Delete User");
    }
    
    private static void EditUser()
    {
        Console.Clear();
        Console.WriteLine("Stub - Edit User");
    }
    
    private static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("Stub - List Users");
    }

    private static void TripsMenu()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("1 - Unos novog putovanja");
            Console.WriteLine("2 - Brisanje putovanja");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja");
            Console.WriteLine("4 - Pregled svih putovanja");
            Console.WriteLine("5 - Izvještaji i analize");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            switch (Console.ReadLine())
            {
                case "1":
                    AddTrip();
                    continue;
                case "2":
                    DeleteTrip();
                    continue;
                case "3":
                    EditTrip();
                    continue;
                case "4":
                    ListTrips();
                    continue;
                case "5":
                    ReportsAndAnalysis();
                    continue;
                case "0":
                    return;
            }
            
            Console.WriteLine("Neispravan unos, pokusajte ponovo!");
        }
    }
    
    private static void AddTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Add Trip");
    }
    
    private static void DeleteTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Delete Trip");
    }
    
    private static void EditTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Edit Trip");
    }
    
    private static void ListTrips()
    {
        Console.Clear();
        Console.WriteLine("Stub - List Trips");
    }
    
    private static void ReportsAndAnalysis()
    {
        Console.Clear();
        Console.WriteLine("Stub - Reports and Analysis");
    }
}