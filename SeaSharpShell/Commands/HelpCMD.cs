namespace SeaSharpShell.Commands;

public class HelpCMD
{
    public static void ShowHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("help - Show this help message");
        Console.WriteLine("age - Calculate your age");
        Console.WriteLine("cd [directory] - Change the current directory");
        Console.WriteLine("mkdir [directory] - Creates a directory");
        Console.WriteLine("rmdir [directory] - Removes the desired directory");
        Console.WriteLine("ls - Show files (yellow) and directories (green)");
        Console.WriteLine("touch [filename] - Creates a file");
        Console.WriteLine("read [filename] - Reads a file");
        Console.WriteLine("open [filename] - Opens a file");
        Console.WriteLine("rename [filename] [new filename] - Renames a file");
        Console.WriteLine("mv [filename] [directory] - Moves a file to the specified directory");
        Console.WriteLine("cp [filename] [directory] - Copies a file to the specified directory");
        Console.WriteLine("delete [filename] - Deletes a file");
        Console.WriteLine("clear - Clear the console screen");
        Console.WriteLine("quit - Exit the program");
        Console.WriteLine("exit - Exit the program");
    }
}