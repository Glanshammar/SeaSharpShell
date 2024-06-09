namespace SeaSharpShell.Commands;

public class HelpCMD
{
    public static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("help - Show this help message");
        Console.WriteLine("hello - Print 'Hello World!'");
        Console.WriteLine("age - Calculate your age");
        Console.WriteLine("clear - Clear the console screen");
        Console.WriteLine("quit - Exit the program");
        Console.WriteLine("exit - Exit the program");
    }
}