using SeaSharpShell.Commands;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "SeaSharpShell";
        Dictionary<string, Action> commandActions = new Dictionary<string, Action>()
        {
            { "help", () => HelpCMD.ShowHelp() },
            { "hello", () => Console.WriteLine("Hello World!") },
            { "age", () => AgeCMD.Age() },
            { "clear", () => Console.Clear() },
            { "quit", () => Environment.Exit(0) },
            { "exit", () => Environment.Exit(0) },
        };
        
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
}