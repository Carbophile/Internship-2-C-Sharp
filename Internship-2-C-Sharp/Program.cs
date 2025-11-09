using System.Globalization;

namespace Internship_2_C_Sharp;

public class User
{
    private static int _nextId = 1;
    
    public int Id { get; }
    public string FName { get; set; }
    public string LName { get; set; }
    public DateTime DOB { get; set; }
    public List<Trip> Trips { get; set; }

    public User(string fName, string lName, DateTime dob)
    {
        Id = _nextId++;
        FName = fName;
        LName = lName;
        DOB = dob;
        Trips = new List<Trip>();
    }
}

public class Trip
{
    private static int _nextId = 1;
    
    public int Id { get; }
    public DateTime Date { get; set; }
    public decimal Distance { get; set; }
    public decimal FuelConsumed { get; set; }
    public decimal FuelCost { get; set; }
    public decimal TotalCost => FuelConsumed * FuelCost;
}


public static class Program
{
    private static List<User> _users = [];
    
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
            
            Console.Clear();
            
            Console.WriteLine("Neispravan unos, pritisnite bilo koju tipku za nastavak!");
            Console.ReadKey();
        }
    }

    private static void UsersMenu()
    {
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("1 - Unos novog korisnika");
            Console.WriteLine("2 - Brisanje korisnika");
            Console.WriteLine("3 - Uređivanje korisnika");
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
            
            Console.Clear();
            Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
            Console.ReadKey();
        }
    }
    
    private static void AddUser()
    {
        while (true)
        {
            Console.Clear();
            
            string? fName;
            string? lName;
            DateTime dOB;

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite ime novog korisnika (ili 0 za odustajanje): ");
                fName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(fName))
                {
                    Console.Clear();
                    Console.WriteLine("Ime ne smije biti prazno! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                if (fName == "0")
                {
                    return;
                }
                
                break;
            }
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite prezime novog korisnika (ili 0 za odustajanje): ");
                lName = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(lName))
                {
                    Console.Clear();
                    Console.WriteLine("Prezime ne smije biti prazno! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                if (lName == "0")
                {
                    return;
                }
                
                break;
            }
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite datum rođenja novog korisnika (ISO 8601 format - yyyy-MM-dd) (ili 0 za odustajanje): ");
                string? dobInput = Console.ReadLine();
                
                if (dobInput == "0")
                {
                    return;
                }
                
               
                if (!DateTime.TryParseExact(dobInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dOB))
                {
                    Console.Clear();
                    Console.WriteLine("Greška pri parsiranju formata! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }

                if (dOB > DateTime.Today)
                {
                    Console.Clear();
                    Console.WriteLine("Datum rođenja ne može biti u budućnosti! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine($"Ime: {fName}\nPrezime: {lName}\nDatum rođenja: {dOB:yyyy-MM-dd}");
                Console.WriteLine("1 - Potvrdi unos");
                Console.WriteLine("0 - Odustani od unosa");

                switch (Console.ReadLine())
                {
                    case "1":
                        _users.Add(new User(fName, lName, dOB));
                        
                        Console.Clear();
                        
                        Console.WriteLine("Korisnik uspješno dodan! Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                        Console.ReadKey();
                        return;
                    case "0":
                        return;
                    default:
                        Console.Clear();
                        
                        Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                        Console.ReadKey();
                        continue;
                }
            }
        }
    }
    
    private static void DeleteUser()
    {
        while (true)
        {
            Console.Clear();

            User? userToDelete = null;

            Console.WriteLine("1 - Brisanje po imenu i prezimenu");
            Console.WriteLine("2 - Brisanje po ID-u");
            Console.WriteLine("0 - Povratak na izbornik korisnika");

            switch (Console.ReadLine())
            {
                case "1":
                    while (true)
                    {
                        Console.Clear();

                        Console.WriteLine("Unesite ime korisnika kojeg želite obrisati (ili 0 za odustajanje): ");
                        string? fName = Console.ReadLine();

                        if (fName == "0")
                        {
                            break;
                        }
                        
                        Console.Clear();

                        Console.WriteLine("Unesite prezime korisnika kojeg želite obrisati (ili 0 za odustajanje): ");
                        string? lName = Console.ReadLine();

                        if (lName == "0")
                        {
                            break;
                        }

                        userToDelete = _users.FirstOrDefault(u =>
                            u.FName.Equals(fName, StringComparison.OrdinalIgnoreCase) &&
                            u.LName.Equals(lName, StringComparison.OrdinalIgnoreCase));

                        if (userToDelete == null)
                        {
                            Console.Clear();
                            
                            Console.WriteLine(
                                "Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                            Console.ReadKey();
                            continue;
                        }

                        break;
                    }

                    break;
                case "2":
                    while (true)
                    {
                        Console.Clear();

                        Console.WriteLine("Unesite ID korisnika kojeg želite obrisati (ili 0 za odustajanje): ");
                        string? idInput = Console.ReadLine();

                        if (idInput == "0")
                        {
                            break;
                        }

                        if (!int.TryParse(idInput, out int id))
                        {
                            Console.Clear();
                            
                            Console.WriteLine(
                                "Neispravan unos ID-a! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                            Console.ReadKey();
                            continue;
                        }

                        userToDelete = _users.FirstOrDefault(u => u.Id == id);

                        if (userToDelete == null)
                        {
                            Console.Clear();
                            
                            Console.WriteLine(
                                "Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                            Console.ReadKey();
                            continue;
                        }

                        break;
                    }

                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    
                    Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
            }

            if (userToDelete == null) continue;

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine(
                    $"ID: {userToDelete.Id}\nIme: {userToDelete.FName}\nPrezime: {userToDelete.LName}\nDatum rođenja: {userToDelete.DOB:yyyy-MM-dd}");
                Console.WriteLine("1 - Potvrdi brisanje");
                Console.WriteLine("0 - Odustani od brisanja");

                switch (Console.ReadLine())
                {
                    case "1":
                        _users.Remove(userToDelete);
                        
                        Console.Clear();
                        
                        Console.WriteLine(
                            "Korisnik uspješno obrisan! Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                        Console.ReadKey();
                        return;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                        Console.ReadKey();
                        continue;
                }
            }
        }
    }
    
    private static void EditUser()
    {
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("Unesite ID korisnika kojeg želite urediti (ili 0 za odustajanje): ");
            string? idInput = Console.ReadLine();
            
            if (idInput == "0")
            {
                return;
            }
            
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Neispravan unos ID-a! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                Console.ReadKey();
                continue;
            }
            
            User? userToEdit = _users.FirstOrDefault(u => u.Id == id);
            
            if (userToEdit == null)
            {
                Console.Clear();
                
                Console.WriteLine("Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                Console.ReadKey();
                continue;
            }
            
            User tempUser = new User(userToEdit.FName, userToEdit.LName, userToEdit.DOB);

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine(
                    $"(1) ID: {userToEdit.Id}\n(2) Ime: {tempUser.FName}\n(3) Prezime: {tempUser.LName}\n(4) Datum rođenja: {tempUser.DOB:yyyy-MM-dd}\n(0) Spremi i izađi\n(-1) Odustani od uređivanja");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        
                        Console.WriteLine("ID nije moguće mijenjati! Pritisnite bilo koju tipku kako biste nastavili.");
                        Console.ReadKey();
                        continue;
                    case "2":
                        Console.Clear();
                        
                        Console.WriteLine("Unesite novo ime: ");
                        string? newFName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newFName))
                        {
                            tempUser.FName = newFName;
                        }
                        else
                        {
                            Console.WriteLine("Ime ne smije biti prazno! Pritisnite bilo koju tipku kako biste nastavili.");
                            Console.ReadKey();
                        }

                        continue;
                    case "3":
                        Console.Clear();
                        
                        Console.WriteLine("Unesite novo prezime: ");
                        string? newLName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newLName))
                        {
                            tempUser.LName = newLName;
                        }
                        else
                        {
                            Console.WriteLine("Prezime ne smije biti prazno! Pritisnite bilo koju tipku kako biste nastavili.");
                            Console.ReadKey();
                        }

                        continue;
                    case "4":
                        Console.Clear();
                        
                        Console.WriteLine("Unesite novi datum rođenja (ISO 8601 format - yyyy-MM-dd): ");
                        string? newDobInput = Console.ReadLine();
                        if (DateTime.TryParseExact(newDobInput, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                DateTimeStyles.None, out DateTime newDOB))
                        {
                            if (newDOB > DateTime.Today)
                            {
                                Console.WriteLine("Datum rođenja ne može biti u budućnosti! Pritisnite bilo koju tipku kako biste nastavili.");
                                Console.ReadKey();
                            }
                            else
                            {
                                tempUser.DOB = newDOB;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Greška pri parsiranju formata! Pritisnite bilo koju tipku kako biste nastavili.");
                            Console.ReadKey();
                        }

                        continue;
                    case "0":
                        userToEdit.FName = tempUser.FName;
                        userToEdit.LName = tempUser.LName;
                        userToEdit.DOB = tempUser.DOB;
                        
                        Console.Clear();
                        
                        Console.WriteLine(
                            "Promjene su uspješno spremljene! Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                        Console.ReadKey();
                        return;
                    case "-1":
                        return;
                    default:
                        Console.Clear();
                        
                        Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                        Console.ReadKey();
                        continue;
                }
            }
        }
    }
    
    private static void ListUsers()
    {
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("1 - Pregled svih korisnika");
            Console.WriteLine("2 - Pregled korisnika starijih od 20 godina");
            Console.WriteLine("3 - Pregled korisnika s najmanje 2 putovanja");
            Console.WriteLine("0 - Povratak na izbornik korisnika");

            switch (Console.ReadLine())
            {
                case "1":
                    var sortedUsers = _users.OrderBy(u => u.LName).ThenBy(u => u.FName).ToList();
                    foreach (var user in sortedUsers)
                    {
                        Console.Clear();

                        Console.WriteLine($"{user.Id} - {user.FName} - {user.LName} - {user.DOB}");
                    }
                    
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                    Console.ReadKey();

                    break;
                case "2":
                    var usersOlderThan20 = _users
                        .Where(u => (DateTime.Now - u.DOB).TotalDays / 365.25 > 20)
                        .OrderBy(u => u.LName)
                        .ThenBy(u => u.FName)
                        .ToList();
                    foreach (var user in usersOlderThan20)
                    {
                        Console.Clear();

                        Console.WriteLine($"{user.Id} - {user.FName} - {user.LName} - {user.DOB}");
                    }
                    
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                    Console.ReadKey();

                    break;
                case "3":
                    var usersWithAtLeast2Trips = _users
                        .Where(u => u.Trips.Count >= 2)
                        .OrderBy(u => u.LName)
                        .ThenBy(u => u.FName)
                        .ToList();
                    foreach (var user in usersWithAtLeast2Trips)
                    {
                        Console.Clear();

                        Console.WriteLine(
                            $"{user.Id} - {user.FName} - {user.LName} - {user.DOB} - Broj putovanja: {user.Trips.Count}");
                    }
                    
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                    Console.ReadKey();
                    
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
            }
        }
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
            
            Console.Clear();
            
            Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
            Console.ReadKey();
        }
    }
    
    private static void AddTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Add Trip");
        Console.ReadKey();
    }
    
    
    private static void DeleteTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Delete Trip");
        Console.ReadKey();
    }
    
    private static void EditTrip()
    {
        Console.Clear();
        Console.WriteLine("Stub - Edit Trip");
        Console.ReadKey();
    }
    
    private static void ListTrips()
    {
        Console.Clear();
        Console.WriteLine("Stub - List Trips");
        Console.ReadKey();
    }
    
    private static void ReportsAndAnalysis()
    {
        Console.Clear();
        Console.WriteLine("Stub - Reports and Analysis");
        Console.ReadKey();
    }
}