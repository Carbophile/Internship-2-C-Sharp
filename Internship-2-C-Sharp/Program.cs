using System.Globalization;

namespace Internship_2_C_Sharp;

public static class Program
{
    private static readonly List<(int Id, string FName, string LName, DateTime DOB)> Users = [];
    private static readonly List<(int Id, int UserId, DateTime Date, decimal Distance, decimal FuelConsumed, decimal FuelCost)> Trips = [];
    private static int _nextUserId = 1;
    private static int _nextTripId = 1;
    
    public static void Main()
    {
        
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("1 - Korisnici");
            Console.WriteLine("2 - Putovanja");
            Console.WriteLine("3 - Statistika");
            Console.WriteLine("0 - Izlaz iz aplikacije");

            switch (Console.ReadLine())
            {
                case "1":
                    UsersMenu();
                    continue;
                case "2":
                    TripsMenu();
                    continue;
                case "3":
                    StatisticsMenu();
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
                        Users.Add((_nextUserId++, fName, lName, dOB));
                        
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

            (int Id, string FName, string LName, DateTime DOB)? userToDelete = null;

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

                        userToDelete = Users.FirstOrDefault(u =>
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

                        userToDelete = Users.FirstOrDefault(u => u.Id == id);

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
                    $"ID: {userToDelete.Value.Id}\nIme: {userToDelete.Value.FName}\nPrezime: {userToDelete.Value.LName}\nDatum rođenja: {userToDelete.Value.DOB:yyyy-MM-dd}");
                Console.WriteLine("1 - Potvrdi brisanje");
                Console.WriteLine("0 - Odustani od brisanja");

                switch (Console.ReadLine())
                {
                    case "1":
                        Users.Remove(userToDelete.Value);
                        Trips.RemoveAll(t => t.UserId == userToDelete.Value.Id);
                        
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
            
            var userToEdit = Users.FirstOrDefault(u => u.Id == id);
            
            if (userToEdit == default)
            {
                Console.Clear();
                
                Console.WriteLine("Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                Console.ReadKey();
                continue;
            }
            
            var tempUser = userToEdit;

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
                        var userIndex = Users.FindIndex(u => u.Id == userToEdit.Id);
                        if (userIndex != -1)
                        {
                            Users[userIndex] = tempUser;
                        }
                        
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
                    var sortedUsers = Users.OrderBy(u => u.LName).ThenBy(u => u.FName).ToList();
                    Console.Clear();
                    foreach (var user in sortedUsers)
                    {
                        Console.WriteLine($"{user.Id} - {user.FName} - {user.LName} - {user.DOB:yyyy-MM-dd}");
                    }
                    
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                    Console.ReadKey();

                    break;
                case "2":
                    var usersOlderThan20 = Users
                        .Where(u => (DateTime.Now - u.DOB).TotalDays / 365.25 > 20)
                        .OrderBy(u => u.LName)
                        .ThenBy(u => u.FName)
                        .ToList();
                    Console.Clear();
                    foreach (var user in usersOlderThan20)
                    {
                        Console.WriteLine($"{user.Id} - {user.FName} - {user.LName} - {user.DOB:yyyy-MM-dd}");
                    }
                    
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak na izbornik korisnika.");
                    Console.ReadKey();

                    break;
                case "3":
                    var usersWithAtLeast2Trips = Users
                        .Where(u => Trips.Count(t => t.UserId == u.Id) >= 2)
                        .OrderBy(u => u.LName)
                        .ThenBy(u => u.FName)
                        .ToList();
                    Console.Clear();
                    foreach (var user in usersWithAtLeast2Trips)
                    {
                        Console.WriteLine(
                            $"{user.Id} - {user.FName} - {user.LName} - {user.DOB:yyyy-MM-dd} - Broj putovanja: {Trips.Count(t => t.UserId == user.Id)}");
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

            (int Id, string FName, string LName, DateTime DOB) user;
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
                
                var foundUser = Users.FirstOrDefault(u => u.Id == userId);
                
                if (foundUser == default)
                {
                    Console.Clear();
                    
                    Console.WriteLine("Korisnik nije pronađen! Pritisnite bilo koju tipku kako biste pokušali ponovo.");
                    Console.ReadKey();
                    continue;
                }
                user = foundUser;
                
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
                        Trips.Add((_nextTripId++, user.Id, date, distance, fuelConsumed, fuelCost));

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

            if (Trips.Count == 0)
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

                    var tripToDelete = Trips.FirstOrDefault(t => t.Id == tripId);
                    if (tripToDelete == default)
                    {
                        Console.WriteLine("Putovanje nije pronađeno. Pritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    }

                    Trips.Remove(tripToDelete);
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

                    int countCheaper = Trips.RemoveAll(t => (t.FuelConsumed * t.FuelCost) < amountCheaper);
                    
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
                    
                    int countMoreExpensive = Trips.RemoveAll(t => (t.FuelConsumed * t.FuelCost) > amountMoreExpensive);

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

        if (Trips.Count == 0)
        {
            Console.WriteLine("Nema unesenih putovanja. Pritisnite bilo koju tipku za povratak.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Sva putovanja:");
        foreach (var trip in Trips.OrderBy(t => t.Id))
        {
            var user = Users.First(u => u.Id == trip.UserId);
            Console.WriteLine($"ID: {trip.Id}, Korisnik: {user.FName} {user.LName}, Datum: {trip.Date:yyyy-MM-dd}, Cijena: {(trip.FuelConsumed * trip.FuelCost):C}");
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

        var tripToEdit = Trips.FirstOrDefault(t => t.Id == tripId);
        if (tripToEdit == default)
        {
            Console.WriteLine("Putovanje nije pronađeno. Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey();
            return;
        }

        var tempTrip = tripToEdit;
        var tripUser = Users.First(u => u.Id == tripToEdit.UserId);

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Korisnik: {tripUser.FName} {tripUser.LName} (ne može se mijenjati)");
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
                    var tripIndex = Trips.FindIndex(t => t.Id == tripToEdit.Id);
                    if (tripIndex != -1)
                    {
                        Trips[tripIndex] = tempTrip;
                    }
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

        if (Trips.Count == 0)
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

            List<(int Id, int UserId, DateTime Date, decimal Distance, decimal FuelConsumed, decimal FuelCost)> sortedTrips = Trips;

            switch (sortChoice)
            {
                case "1":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.Id).ToList() : sortedTrips.OrderByDescending(t => t.Id).ToList();
                    break;
                case "2":
                    sortedTrips = ascending ? sortedTrips.OrderBy(t => t.FuelConsumed * t.FuelCost).ToList() : sortedTrips.OrderByDescending(t => t.FuelConsumed * t.FuelCost).ToList();
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
                Console.WriteLine($"Putovanje #{trip.Id}\nDatum: {trip.Date:yyyy-MM-dd}\nKilometri: {trip.Distance}\nGorivo: {trip.FuelConsumed} L\nCijena po litri: {trip.FuelCost:C} \nUkupno: {(trip.FuelConsumed * trip.FuelCost):C}"); 
            }
            
            Console.WriteLine("\nPritisnite bilo koju tipku za povratak na odabir sortiranja.");
            Console.ReadKey();
        }
    }
    
    private static void ReportsAndAnalysis()
    {
        while (true)
        {
            Console.Clear();

            if (Users.Count == 0)
            {
                Console.WriteLine("Nema unesenih korisnika. Pritisnite bilo koju tipku za povratak.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Popis svih korisnika:");
            foreach (var u in Users)
            {
                Console.WriteLine($"ID: {u.Id}, Ime: {u.FName} {u.LName}");
            }

            Console.WriteLine("\nUnesite ID korisnika za kojeg želite izvještaj (ili 0 za povratak):");
            string? userIdInput = Console.ReadLine();

            if (userIdInput == "0")
            {
                return;
            }

            if (!int.TryParse(userIdInput, out int userId))
            {
                Console.WriteLine("Neispravan unos ID-a. Pritisnite bilo koju tipku za ponovni pokušaj.");
                Console.ReadKey();
                continue;
            }

            var selectedUser = Users.FirstOrDefault(u => u.Id == userId);

            if (selectedUser == default)
            {
                Console.WriteLine("Korisnik nije pronađen. Pritisnite bilo koju tipku za ponovni pokušaj.");
                Console.ReadKey();
                continue;
            }
            
            var userTrips = Trips.Where(t => t.UserId == selectedUser.Id).ToList();

            if (userTrips.Count == 0)
            {
                Console.WriteLine("Odabrani korisnik nema unesenih putovanja. Pritisnite bilo koju tipku za povratak.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Izvještaji za korisnika: {selectedUser.FName} {selectedUser.LName}");
                Console.WriteLine("1 - Ukupna potrošnja goriva");
                Console.WriteLine("2 - Ukupni troškovi goriva");
                Console.WriteLine("3 - Prosječna potrošnja goriva u L/100km");
                Console.WriteLine("4 - Putovanje s najvećom potrošnjom goriva");
                Console.WriteLine("5 - Pregled putovanja po određenom datumu");
                Console.WriteLine("0 - Povratak na izbornik putovanja");

                switch (Console.ReadLine())
                {
                    case "1":
                        decimal totalFuel = userTrips.Sum(t => t.FuelConsumed);
                        Console.Clear();
                        Console.WriteLine($"Ukupna potrošnja goriva: {totalFuel} L");
                        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    case "2":
                        decimal totalCost = userTrips.Sum(t => t.FuelConsumed * t.FuelCost);
                        Console.Clear();
                        Console.WriteLine($"Ukupni troškovi goriva: {totalCost:C}");
                        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    case "3":
                        decimal totalFuelForAvg = userTrips.Sum(t => t.FuelConsumed);
                        decimal totalDistance = userTrips.Sum(t => t.Distance);
                        Console.Clear();
                        if (totalDistance > 0)
                        {
                            decimal averageConsumption = (totalFuelForAvg / totalDistance) * 100;
                            Console.WriteLine($"Prosječna potrošnja goriva: {averageConsumption:F2} L/100km");
                        }
                        else
                        {
                            Console.WriteLine("Nije moguće izračunati prosječnu potrošnju jer ukupna udaljenost je 0.");
                        }
                        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    case "4":
                        var tripWithHighestConsumption = userTrips.OrderByDescending(t => t.FuelConsumed).FirstOrDefault();
                        Console.Clear();
                        if (tripWithHighestConsumption != default)
                        {
                            Console.WriteLine("Putovanje s najvećom potrošnjom goriva:");
                            Console.WriteLine($"ID: {tripWithHighestConsumption.Id}, Datum: {tripWithHighestConsumption.Date:yyyy-MM-dd}, Udaljenost: {tripWithHighestConsumption.Distance} km, Potrošeno gorivo: {tripWithHighestConsumption.FuelConsumed} L, Ukupni trošak: {(tripWithHighestConsumption.FuelConsumed * tripWithHighestConsumption.FuelCost):C}");
                        }
                        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Unesite datum za pregled putovanja (yyyy-MM-dd):");
                        string? dateInput = Console.ReadLine();
                        if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime searchDate))
                        {
                            var tripsOnDate = userTrips.Where(t => t.Date.Date == searchDate.Date).ToList();
                            Console.Clear();
                            if (tripsOnDate.Any())
                            {
                                Console.WriteLine($"Putovanja na dan {searchDate:yyyy-MM-dd}:");
                                foreach (var trip in tripsOnDate)
                                {
                                    Console.WriteLine($"ID: {trip.Id}, Udaljenost: {trip.Distance} km, Potrošeno gorivo: {trip.FuelConsumed} L, Ukupni trošak: {(trip.FuelConsumed * trip.FuelCost):C}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Nema putovanja na dan {searchDate:yyyy-MM-dd}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Neispravan format datuma.");
                        }
                        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
                        Console.ReadKey();
                        continue;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Neispravan unos. Pritisnite bilo koju tipku za ponovni pokušaj.");
                        Console.ReadKey();
                        continue;
                }
                break;
            }
        }
    }
    
    private static void StatisticsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Statistika");
            Console.WriteLine("1 - Korisnik s najvećim ukupnim troškom goriva");
            Console.WriteLine("2 - Korisnik s najviše putovanja");
            Console.WriteLine("3 - Prosječan broj putovanja po korisniku");
            Console.WriteLine("4 - Ukupan broj prijeđenih kilometara svih korisnika");
            Console.WriteLine("0 - Povratak na glavni izbornik");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowUserWithHighestTotalFuelCost();
                    continue;
                case "2":
                    ShowUserWithMostTrips();
                    continue;
                case "3":
                    ShowAverageTripsPerUser();
                    continue;
                case "4":
                    ShowTotalDistanceAllUsers();
                    continue;
                case "0":
                    return;
                default:
                    Console.WriteLine("Neispravan unos. Pritisnite bilo koju tipku za ponovni pokušaj.");
                    Console.ReadKey();
                    continue;
            }
        }
    }

    private static void ShowUserWithHighestTotalFuelCost()
    {
        Console.Clear();
        if (Users.Count == 0)
        {
            Console.WriteLine("Nema unesenih korisnika.");
        }
        else
        {
            var userCosts = Users
                .Select(u => new { User = u, TotalCost = Trips.Where(t => t.UserId == u.Id).Sum(t => t.FuelConsumed * t.FuelCost) })
                .ToList();

            var maxCost = userCosts.Max(uc => uc.TotalCost);

            if (maxCost <= 0)
            {
                Console.WriteLine("Nema korisnika s troškovima goriva (nema putovanja).");
            }
            else
            {
                var topUsers = userCosts.Where(uc => uc.TotalCost == maxCost).Select(uc => uc.User).ToList();
                Console.WriteLine("Korisnik(i) s najvećim ukupnim troškom goriva:");
                foreach (var u in topUsers)
                {
                    Console.WriteLine($"ID: {u.Id}, {u.FName} {u.LName} - Ukupni trošak: {Trips.Where(t => t.UserId == u.Id).Sum(t => t.FuelConsumed * t.FuelCost):C} - Broj putovanja: {Trips.Count(t => t.UserId == u.Id)}");
                }
            }
        }

        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
        Console.ReadKey();
    }

    private static void ShowUserWithMostTrips()
    {
        Console.Clear();
        if (Users.Count == 0)
        {
            Console.WriteLine("Nema unesenih korisnika.");
        }
        else
        {
            var userWithMostTrips = Users.Select(u => new { User = u, TripCount = Trips.Count(t => t.UserId == u.Id) })
                                         .OrderByDescending(x => x.TripCount)
                                         .FirstOrDefault();

            if (userWithMostTrips != null && userWithMostTrips.TripCount > 0)
            {
                Console.WriteLine($"Korisnik s najviše putovanja: {userWithMostTrips.User.FName} {userWithMostTrips.User.LName} ({userWithMostTrips.TripCount} putovanja)");
            }
            else
            {
                Console.WriteLine("Nema korisnika s putovanjima.");
            }
        }
        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
        Console.ReadKey();
    }

    private static void ShowAverageTripsPerUser()
    {
        Console.Clear();
        if (Users.Count == 0)
        {
            Console.WriteLine("Nema unesenih korisnika.");
        }
        else
        {
            int totalTrips = Trips.Count;
            decimal average = Users.Count > 0 ? (decimal)totalTrips / Users.Count : 0m;
            Console.WriteLine($"Prosječan broj putovanja po korisniku: {average:F2}");
            Console.WriteLine($"Ukupan broj putovanja: {totalTrips}");
            Console.WriteLine($"Broj korisnika: {Users.Count}");
        }

        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
        Console.ReadKey();
    }

    private static void ShowTotalDistanceAllUsers()
    {
        Console.Clear();
        if (Trips.Count == 0)
        {
            Console.WriteLine("Nema unesenih putovanja.");
        }
        else
        {
            decimal totalDistance = Trips.Sum(t => t.Distance);
            Console.WriteLine($"Ukupan broj prijeđenih kilometara svih korisnika: {totalDistance} km");
        }

        Console.WriteLine("\nPritisnite bilo koju tipku za nastavak.");
        Console.ReadKey();
    }
}
