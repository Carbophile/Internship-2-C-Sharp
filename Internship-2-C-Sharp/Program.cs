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
    public User User { get; set; }
    public DateTime Date { get; set; }
    public decimal Distance { get; set; }
    public decimal FuelConsumed { get; set; }
    public decimal FuelCost { get; set; }
    public decimal TotalCost => FuelConsumed * FuelCost;
    
    public Trip(User user, DateTime date, decimal distance, decimal fuelConsumed, decimal fuelCost)
    {
        Id = _nextId++;
        User = user;
        Date = date;
        Distance = distance;
        FuelConsumed = fuelConsumed;
        FuelCost = fuelCost;
    }
}


public static class Program
{
    private static List<User> _users = [];
    private static List<Trip> _trips = [];
    
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
                        _trips.RemoveAll(t => t.User == userToDelete);
                        
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

        while (true)
        {
            Console.Clear();

            User? user;
            DateTime date;
            decimal distance;
            decimal fuelConsumed;
            decimal fuelCost;

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite ID korisnika za kojeg unosite putovanje (ili 0 za odustajanje): ");
                string? userIdInput = Console.ReadLine();
                
                if (userIdInput == "0")
                {
                    return;
                }
                
                if (!int.TryParse(userIdInput, out int userId))
                {
                    Console.Clear();
                    
                    Console.WriteLine("Neispravan unos ID-a! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                user = _users.FirstOrDefault(u => u.Id == userId);
                
                if (user == null)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite datum putovanja (ISO 8601 format - yyyy-MM-dd) (ili 0 za odustajanje): ");
                string? dateInput = Console.ReadLine();
                
                if (dateInput == "0")
                {
                    return;
                }
                
                if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    Console.Clear();
                    
                    Console.WriteLine("Greška pri parsiranju formata! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                if (date > DateTime.Today)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Datum putovanja ne može biti u budućnosti! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }

            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite prijeđenu udaljenost u kilometrima (ili 0 za odustajanje): ");
                string? distanceInput = Console.ReadLine();
                
                if (distanceInput == "0")
                {
                    return;
                }
                
                if (!decimal.TryParse(distanceInput, out distance) || distance <= 0)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Neispravan unos udaljenosti! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite potrošenu količinu goriva u litrama (ili 0 za odustajanje): ");
                string? fuelConsumedInput = Console.ReadLine();
                
                if (fuelConsumedInput == "0")
                {
                    return;
                }
                
                if (!decimal.TryParse(fuelConsumedInput, out fuelConsumed) || fuelConsumed <= 0)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Neispravan unos količine goriva! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }
            
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Unesite cijenu goriva po litri (ili 0 za odustajanje): ");
                string? fuelCostInput = Console.ReadLine();
                
                if (fuelCostInput == "0")
                {
                    return;
                }
                
                if (!decimal.TryParse(fuelCostInput, out fuelCost) || fuelCost <= 0)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Neispravan unos cijene goriva! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                
                break;
            }

            while (true)
            {
                Console.Clear();

                Console.WriteLine(
                    $"Korisnik: {user.FName} {user.LName}\nDatum putovanja: {date:yyyy-MM-dd}\nUdaljenost: {distance} km\nPotrošeno gorivo: {fuelConsumed} L\nCijena goriva po litri: {fuelCost:C}\nUkupni trošak: {(fuelConsumed * fuelCost):C}");
                Console.WriteLine("1 - Potvrdi unos");
                Console.WriteLine("0 - Odustani od unosa");

                switch (Console.ReadLine())
                {
                    case "1":
                        Trip newTrip = new Trip(user, date, distance, fuelConsumed, fuelCost);
                        user.Trips.Add(newTrip);
                        _trips.Add(newTrip);

                        Console.Clear();

                        Console.WriteLine(
                            "Putovanje uspješno dodano! Pritisnite bilo koju tipku za povratak na izbornik putovanja.");
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
    
    
    private static void DeleteTrip()
    {
        while (true)
        {
            Console.Clear();

            if (_trips.Count == 0)
            {
                Console.WriteLine("Nema unesenih putovanja. Pritisnite bilo koju tipku za povratak.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("1 - Brisanje po ID-u putovanja");
            Console.WriteLine("2 - Brisanje putovanja jeftinijih od unesenog iznosa");
            Console.WriteLine("3 - Brisanje putovanja skupljih od unesenog iznosa");
            Console.WriteLine("0 - Povratak na izbornik putovanja");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Unesite ID putovanja za brisanje (ili 0 za odustajanje): ");
                    string? tripIdInput = Console.ReadLine();
                    if (tripIdInput == "0") continue;

                    if (!int.TryParse(tripIdInput, out int tripId))
                    {
                        Console.WriteLine("Neispravan ID. Pritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    }

                    Trip? tripToDelete = _trips.FirstOrDefault(t => t.Id == tripId);
                    if (tripToDelete == null)
                    {
                        Console.WriteLine("Putovanje nije pronađeno. Pritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    }

                    tripToDelete.User.Trips.Remove(tripToDelete);
                    _trips.Remove(tripToDelete);
                    Console.WriteLine("Putovanje uspješno obrisano. Pritisnite bilo koju tipku za nastavak.");
                    Console.ReadKey();
                    return;

                case "2":
                    Console.WriteLine("Unesite iznos. Sva putovanja jeftinija od ovog iznosa bit će obrisana: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amountCheaper))
                    {
                        Console.WriteLine("Neispravan unos iznosa. Pritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    }

                    var tripsToRemoveCheaper = _trips.Where(t => t.TotalCost < amountCheaper).ToList();
                    foreach (var trip in tripsToRemoveCheaper)
                    {
                        trip.User.Trips.Remove(trip);
                    }
                    int countCheaper = _trips.RemoveAll(t => t.TotalCost < amountCheaper);
                    
                    Console.WriteLine($"{countCheaper} putovanja obrisano. Pritisnite bilo koju tipku za nastavak.");
                    Console.ReadKey();
                    return;

                case "3":
                    Console.WriteLine("Unesite iznos. Sva putovanja skuplja od ovog iznosa bit će obrisana: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amountMoreExpensive))
                    {
                        Console.WriteLine("Neispravan unos iznosa. Pritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    }
                    
                    var tripsToRemoveMoreExpensive = _trips.Where(t => t.TotalCost > amountMoreExpensive).ToList();
                    foreach (var trip in tripsToRemoveMoreExpensive)
                    {
                        trip.User.Trips.Remove(trip);
                    }
                    int countMoreExpensive = _trips.RemoveAll(t => t.TotalCost > amountMoreExpensive);

                    Console.WriteLine($"{countMoreExpensive} putovanja obrisano. Pritisnite bilo koju tipku za nastavak.");
                    Console.ReadKey();
                    return;

                case "0":
                    return;

                default:
                    Console.WriteLine("Neispravan unos! Pritisnite bilo koju tipku za ponovni pokušaj.");
                    Console.ReadKey();
                    continue;
            }
        }
    }
    
    private static void EditTrip()
    {
        Console.Clear();

        if (_trips.Count == 0)
        {
            Console.WriteLine("Nema unesenih putovanja. Pritisnite bilo koju tipku za povratak.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Sva putovanja:");
        foreach (var trip in _trips.OrderBy(t => t.Id))
        {
            Console.WriteLine($"ID: {trip.Id}, Korisnik: {trip.User.FName} {trip.User.LName}, Datum: {trip.Date:yyyy-MM-dd}, Cijena: {trip.TotalCost:C}");
        }

        Console.WriteLine("\nUnesite ID putovanja koje želite urediti (ili 0 za odustajanje): ");
        string? tripIdInput = Console.ReadLine();
        if (tripIdInput == "0") return;

        if (!int.TryParse(tripIdInput, out int tripId))
        {
            Console.WriteLine("Neispravan ID. Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey();
            return;
        }

        Trip? tripToEdit = _trips.FirstOrDefault(t => t.Id == tripId);
        if (tripToEdit == null)
        {
            Console.WriteLine("Putovanje nije pronađeno. Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey();
            return;
        }

        Trip tempTrip = new Trip(tripToEdit.User, tripToEdit.Date, tripToEdit.Distance, tripToEdit.FuelConsumed, tripToEdit.FuelCost);

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Korisnik: {tripToEdit.User.FName} {tripToEdit.User.LName} (ne može se mijenjati)");
            Console.WriteLine($"(1) Datum: {tempTrip.Date:yyyy-MM-dd}\n(2) Udaljenost: {tempTrip.Distance}\n(3) Potrošeno gorivo: {tempTrip.FuelConsumed}\n(4) Cijena goriva: {tempTrip.FuelCost}\n(0) Spremi i izađi\n(-1) Odustani");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Unesite novi datum (yyyy-MM-dd): ");
                    if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newDate) && newDate <= DateTime.Today)
                        tempTrip.Date = newDate;
                    else
                        Console.WriteLine("Neispravan format datuma ili je datum u budućnosti.");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.WriteLine("Unesite novu udaljenost: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newDistance) && newDistance > 0)
                        tempTrip.Distance = newDistance;
                    else
                        Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Unesite novu potrošnju goriva: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newFuelConsumed) && newFuelConsumed > 0)
                        tempTrip.FuelConsumed = newFuelConsumed;
                    else
                        Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Unesite novu cijenu goriva: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newFuelCost) && newFuelCost > 0)
                        tempTrip.FuelCost = newFuelCost;
                    else
                        Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
                case "0":
                    tripToEdit.Date = tempTrip.Date;
                    tripToEdit.Distance = tempTrip.Distance;
                    tripToEdit.FuelConsumed = tempTrip.FuelConsumed;
                    tripToEdit.FuelCost = tempTrip.FuelCost;
                    Console.WriteLine("Promjene spremljene. Pritisnite bilo koju tipku za nastavak.");
                    Console.ReadKey();
                    return;
                case "-1":
                    return;
                default:
                    Console.WriteLine("Neispravan unos.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    
    private static void ListTrips()
    {
        Console.Clear();

        if (_trips.Count == 0)
        {
            Console.WriteLine("Nema unesenih putovanja. Pritisnite bilo koju tipku za povratak.");
            Console.ReadKey();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Odaberite opciju sortiranja:");
            Console.WriteLine("1 - Po ID-u");
            Console.WriteLine("2 - Po cijeni");
            Console.WriteLine("3 - Po udaljenosti");
            Console.WriteLine("4 - Po datumu");
            Console.WriteLine("0 - Povratak");

            string? sortChoice = Console.ReadLine();
            if (sortChoice == "0") return;
            
            Console.Clear();

            Console.WriteLine("1 - Uzlazno");
            Console.WriteLine("2 - Silazno");
            string? orderChoice = Console.ReadLine();
            bool ascending = orderChoice == "1";

            List<Trip> sortedTrips = _trips;

            switch (sortChoice)
            {
                case "1":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.Id).ToList() : sortedTrips.OrderByDescending(t => t.Id).ToList();
                    break;
                case "2":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.TotalCost).ToList() : sortedTrips.OrderByDescending(t => t.TotalCost).ToList();
                    break;
                case "3":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.Distance).ToList() : sortedTrips.OrderByDescending(t => t.Distance).ToList();
                    break;
                case "4":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.Date).ToList() : sortedTrips.OrderByDescending(t => t.Date).ToList();
                    break;
                default:
                    Console.WriteLine("Neispravan unos. Pritisnite bilo koju tipku za nastavak.");
                    Console.ReadKey();
                    continue;
            }

            Console.Clear();
            
            foreach (var trip in sortedTrips)
            {
                Console.WriteLine($"Putovanje #{trip.Id}\nDatum: {trip.Date:yyyy-MM-dd}\nKilometri: {trip.Distance}\nGorivo: {trip.FuelConsumed} L\nCijena po litri: {trip.FuelCost:C} \nUkupno: {trip.TotalCost:C}"); 
            }
            
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak na odabir sortiranja.");
            Console.ReadKey();
        }
    }
    
    private static void ReportsAndAnalysis()
    {
        Console.Clear();
        Console.WriteLine("Stub - Reports and Analysis");
        Console.ReadKey();
    }
}