using SeaSharpShell.Commands;

class Program
{
    static Dictionary<string, Action> commandActions = new Dictionary<string, Action>()
    {
        { "help", () => ShowHelp() },
        { "hello", () => Console.WriteLine("Hello World!") },
        { "age", () => AgeCMD.Age() },
        { "clear", () => Console.Clear() },
        { "quit", () => Environment.Exit(0) },
        { "exit", () => Environment.Exit(0) },
    };

    static void Main(string[] args)
    {
        Console.Title = "SeaSharpShell";
        bool running = true;

        while (running)
        {
            Console.Write(">> ");
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
}