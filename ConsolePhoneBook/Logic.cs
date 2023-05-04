using Newtonsoft.Json;

namespace ConsolePhoneBook;

class Logic
{
    static List<Contact> Contacts = new();

    internal static void AddContact()
    {
        Console.Clear();
        Console.Write("Podaj nazwę: ");
        string name = Console.ReadLine();
        Console.Write("Podaj numer: ");
        string number = Console.ReadLine();

        int id = Contacts.Count + 1;

        Contacts.Add(new Contact(id, name, number));
    }

    internal static void ModifyContact()
    {
        Console.Clear();
        Console.Write("Podaj ID: ");
        string? userInput = Console.ReadLine();
        var result = Contacts.Find(r => r.ID == int.Parse(userInput));
        if (Contacts.Count == 0)
        {
            Console.WriteLine("Książka jest pusta :(");
        }
        else
        {
            if (result == null)
            {
                Console.WriteLine("Brak wyników");
            }
            else
            {
                Console.WriteLine("Co jaką właściwość kontaktu chcesz zmodyfikować?");
                Console.WriteLine("(1) Nazwa");
                Console.WriteLine("(2) Numer");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.Write("Wprowadź nową nazwę: ");
                        result.Name = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Wprowadź nowy numer: ");
                        result.Number = Console.ReadLine();
                        break;
                    default:
                        Interface.Error("Wprowadzono nieprawidłowe dane");
                        break;
                }
            }
        }
    }

    internal static void ShowContactsList()
    {
        Console.Clear();
        if (Contacts.Count == 0)
        {
            Console.WriteLine("Książka jest pusta :(");
        }
        else
        {
            foreach (var contact in Contacts)
            {
                Console.WriteLine($"ID: {contact.ID} Nazwa: {contact.Name}, numer: {contact.Number}");
            }
        }
    }

    internal static void SearchForAContact()
    {
        Console.Clear();

        if (Contacts.Count == 0)
        {
            Console.WriteLine("Książka jest pusta :(");
        }
        else
        {
            Console.WriteLine("Wyszukaj za pomocą:");
            Console.WriteLine("(1) ID");
            Console.WriteLine("(2) Nazwy");
            Console.WriteLine("(3) Numeru");
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Write("Podaj numer ID: ");
                    string? userChoice = Console.ReadLine();
                    try
                    {
                        var result = Contacts.Find(f => f.ID == int.Parse(userChoice));
                        Console.WriteLine($"{result.ID} {result.Name} {result.Number}");
                    }
                    catch (System.FormatException)
                    {
                        Interface.Error("Wprowadzono błędne dane");
                    }
                    break;

                case "2":
                    try
                    {
                        Console.Write("Podaj nazwę kontaktu: ");
                        userChoice = Console.ReadLine();
                        var results = Contacts.Where(f => f.Name.Contains(userChoice));

                        Interface.Found($"Znaleziono następujące wyniki w ilośći: {results.Count()} ");
                        foreach (var contact in results)
                        {
                            Console.WriteLine($"ID:{contact.ID} Nazwa:{contact.Name} Telefon:{contact.Number}");
                        }
                    }
                    catch (System.FormatException)
                    {
                        Interface.Error("Wprowadzono błędne dane");
                    }
                    break;

                case "3":
                    try
                    {
                        Console.Write("Podaj numer telefonu: ");
                        userChoice = Console.ReadLine();
                        var results = Contacts.Where(f => f.Number.Contains(userChoice));

                        Interface.Found($"Znaleziono następujące wyniki w ilośći: {results.Count()} ");
                        foreach (var contact in results)
                        {
                            Console.WriteLine($"ID: {contact.ID} Nazwa: {contact.Name} Telefon: {contact.Number}");
                        }
                    }
                    catch (System.FormatException)
                    {
                        Interface.Error("Wprowadzono błędne dane");
                    }

                    break;

                default:
                    Interface.Error("Wprowadzono błędne dane");
                    break;
            }
        }
    }

    internal static void RemoveContact()
    {
        Console.Clear();
        Console.Write("Podaj numer ID: ");
        int userInput = int.Parse(Console.ReadLine());
        Contacts.Remove(Contacts[userInput - 1]);
        UpdateContactID(Contacts);
    }

    internal static void Database()
    {
        Console.Clear();
        Console.WriteLine("Wybierz opcję: ");
        Console.WriteLine("(1) Utwórz plik");
        Console.WriteLine("(2) Zaktualizuj plik");
        Console.WriteLine("(3) Usuń plik");
        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                CreateDatabase();
                break;
            case "2":
                LoadDatabase();
                break;
            case "3":
                DeleteDatabase();
                break;
            default:
                Interface.Error("Wprowadzono błędne dane");
                break;
        }
    }

    static void UpdateContactID(List<Contact> Contacts)
    {
        for (int i = 0; i < Contacts.Count; i++)
        {
            Contacts[i].ID = i + 1;
        }
    }

    //Database methods

    static void CreateDatabase()
    {
        Console.WriteLine("Tworzę...");
        string database = JsonConvert.SerializeObject(Contacts, Formatting.Indented);
        File.WriteAllText(@"C:\Users\patry\Desktop\Contacts.json", database);
    }

    static void LoadDatabase()
    {
        Console.WriteLine("Wczytuje...");
        string database = File.ReadAllText(@"C:\Users\patry\Desktop\Contacts.json");
        var loadDatabase = JsonConvert.DeserializeObject<List<Contact>>(database);

        foreach (var contact in loadDatabase)
        {
            Contacts.Add(contact);
        }
    }

    static void DeleteDatabase()
    {
        Console.WriteLine("Usuwam...");
        File.Delete(@"C:\Users\patry\Desktop\Contacts.json");
    }
}
