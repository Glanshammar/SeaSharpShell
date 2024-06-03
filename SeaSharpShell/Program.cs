
class Program
{
    static Dictionary<string, Action> commandActions = new Dictionary<string, Action>()
    {
        { "help", () => ShowHelp() },
        { "hello", () => Console.WriteLine("Hello World!") },
        { "age", () => Age() },
        { "clear", () => Console.Clear() },
        { "quit", () => Environment.Exit(0) },
        { "exit", () => Environment.Exit(0) },
    };

    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Write("Enter a command: ");
            string input = Console.ReadLine().ToLower();

            if (commandActions.ContainsKey(input))
            {
                commandActions[input].Invoke();
            }
            else
            {
                Console.WriteLine("Unknown command. Type 'help' for available commands.");
            }
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        foreach (var command in commandActions.Keys)
        {
            Console.WriteLine(command);
        }
    }
    
    static void Age()
    {
        Console.WriteLine("When are you born? (YYYY-MM-DD)");
        Console.Write("Date: ");
        DateTime dob;

        if (DateTime.TryParse(Console.ReadLine(), out dob))
        {
            int age = CalculateAge(dob);
            Console.WriteLine($"Your age is: {age} years.");
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter your date of birth in YYYY-MM-DD format.");
        }
    }
    
    static int CalculateAge(DateTime dob)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age))
        {
            age--; // User hasn't had birthday yet this year
        }
        return age;
    }
}