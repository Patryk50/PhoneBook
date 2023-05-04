namespace ConsolePhoneBook;

class Interface
{
    internal static void Menu()
    {
        Console.WriteLine("MENU\n");
        Console.WriteLine("(1) Dodaj");
        Console.WriteLine("(2) Modyfikuj");
        Console.WriteLine("(3) Wyświetl spis");
        Console.WriteLine("(4) Wyszukaj");
        Console.WriteLine("(5) Usuń");
        Console.WriteLine("(6) Zarządzanie bazą danych");
        Console.WriteLine("(0) Zakończ");

        Console.Write("Wybierz opcję: ");

        switch (Console.ReadLine())
        {
            case "1":
                Logic.AddContact();
                Run();
                break;
            case "2":
                Logic.ModifyContact();
                Run();
                break;
            case "3":
                Logic.ShowContactsList();
                Run();
                break;
            case "4":
                Logic.SearchForAContact();
                Run();
                break;
            case "5":
                Logic.RemoveContact();
                Run();
                break;
            case "6":
                Logic.Database();
                Run();
                break;
            case "0":
                break;
            default:
                Error("Wprowadzono nieprawidłowe dane...");
                break;
        }
    }

    internal static void Found(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    internal static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    internal static void Run()
    {
        Console.WriteLine("Wciśnij enter aby kontynuować...");
        ConsoleKeyInfo keyPressed = Console.ReadKey(true);
        do
        {
            Console.Clear();
            Menu();
        } while (keyPressed.Key != ConsoleKey.Enter);
    }
}
